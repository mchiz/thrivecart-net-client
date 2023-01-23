namespace ThriveCart {
    public class ConcurrentLimitedTimedAccessResourceGuard : IDisposable {
        public ConcurrentLimitedTimedAccessResourceGuard( int maxConcurrentAccesses, int accessLifeTimeMilliseconds ) {
            _semaphore = new SemaphoreSlim( maxConcurrentAccesses );
            _accessLifeTimeMilliseconds = accessLifeTimeMilliseconds;
        }

        public async Task WaitAsync( CancellationToken cancellationToken = default ) {
            await _semaphore.WaitAsync( cancellationToken );
            ++_usedResourcesCount;

            System.Diagnostics.Trace.WriteLine( $"Concurrent resources> Acquired (used '{_usedResourcesCount}')");

            ScheduleSemaphoreRelease( );
        }

        async void ScheduleSemaphoreRelease( ) {
            async Task DoTheSchedule( ) {
                await Task.Delay( _accessLifeTimeMilliseconds );

                --_usedResourcesCount;

                System.Diagnostics.Trace.WriteLine( $"Concurrent resources> Released (used '{_usedResourcesCount}')");

                _semaphore.Release( );
            }

            var task = DoTheSchedule( );
            
            _scheduledReleaseTasks.Add( task );

            await task;

            _scheduledReleaseTasks.Remove( task );
        }


        async void IDisposable.Dispose( ) {
            await Task.WhenAll( _scheduledReleaseTasks.ToArray( ) );

            _semaphore.Dispose( );
        }

        int _accessLifeTimeMilliseconds;
        SemaphoreSlim _semaphore;
        List< Task > _scheduledReleaseTasks = new List< Task >( );

        int _usedResourcesCount;
    }
}