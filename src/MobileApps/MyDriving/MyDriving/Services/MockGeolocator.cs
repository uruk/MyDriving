using System;
using System.Threading;
using System.Threading.Tasks;
using Plugin.Geolocator.Abstractions;

namespace MyDriving.Services
{
    class MockGeolocator : IGeolocator
    {
        private readonly Position[] _postCircle =
        {
            new Position
            {
                Longitude = 19.0536808,
                Latitude = 47.4737375
            },
            new Position
            {
                Longitude = 19.05368,
                Latitude = 47.47374
            },
            new Position
            {
                Longitude = 19.05384,
                Latitude = 47.47357
            },
            new Position
            {
                Longitude = 19.05394,
                Latitude = 47.4735
            },
            new Position
            {
                Longitude = 19.05399,
                Latitude = 47.47346
            },
            new Position
            {
                Longitude = 19.05403,
                Latitude = 47.47345
            },
            new Position
            {
                Longitude = 19.05405,
                Latitude = 47.47344
            },
            new Position
            {
                Longitude = 19.0541,
                Latitude = 47.47345
            },
            new Position
            {
                Longitude = 19.05435,
                Latitude = 47.47358
            },
            new Position
            {
                Longitude = 19.05437,
                Latitude = 47.4736
            },
            new Position
            {
                Longitude = 19.05439,
                Latitude = 47.47362
            },
            new Position
            {
                Longitude = 19.05439,
                Latitude = 47.47366
            },
            new Position
            {
                Longitude = 19.05438,
                Latitude = 47.47368
            },
            new Position
            {
                Longitude = 19.05415,
                Latitude = 47.47387
            },
            new Position
            {
                Longitude = 19.05407,
                Latitude = 47.47392
            },
            new Position
            {
                Longitude = 19.05368,
                Latitude = 47.47374
            }
        };

        private int _idx;
        private Timer _posChangedTimer;

        public Task<Position> GetPositionAsync(int timeoutMilliseconds = -1, CancellationToken? token = null,
            bool includeHeading = false)
        {
            return Task.FromResult(_postCircle[_idx]);
        }

        public Task<bool> StartListeningAsync(int minTime, double minDistance, bool includeHeading = false)
        {
            IsListening = true;

            _idx = 0;
            _posChangedTimer = new Timer(state =>
            {
                if (_idx == _postCircle.Length)
                {
                    _idx = 0;
                }

                PositionChanged?.Invoke(this, new PositionEventArgs(_postCircle[_idx++]));
            }, null, minTime, minTime);

            return Task.FromResult(true);
        }

        public Task<bool> StopListeningAsync()
        {
            IsListening = false;
            _posChangedTimer.Dispose();
            return Task.FromResult(true);
        }

        public double DesiredAccuracy { get; set; }
        public bool IsListening { get; private set; }
        public bool SupportsHeading { get; } = false;
        public bool AllowsBackgroundUpdates { get; set; }
        public bool PausesLocationUpdatesAutomatically { get; set; }
        public bool IsGeolocationAvailable { get; } = true;
        public bool IsGeolocationEnabled { get; } = true;
        public event EventHandler<PositionErrorEventArgs> PositionError;
        public event EventHandler<PositionEventArgs> PositionChanged;
    }
}