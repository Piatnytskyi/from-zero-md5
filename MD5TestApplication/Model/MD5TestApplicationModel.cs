namespace MD5TestApplication.Model
{
    public class MD5TestApplicationModel : AbstractModel
    {
        private string _enteredInput;

        public string EnteredInput
        {
            get => _enteredInput;
            set
            {
                if (_enteredInput.Equals(value))
                {
                    return;
                }
                _enteredInput = value;
                RaisePropertyChanged(nameof(_enteredInput));
            }
        }

        private string _filenameInput;

        public string FilenameInput
        {
            get => _filenameInput;
            set
            {
                if (_filenameInput.Equals(value))
                {
                    return;
                }
                _filenameInput = value;
                RaisePropertyChanged(nameof(_filenameInput));
            }
        }

        private string _filenameToCheckInput;

        public string FilenameToCheckInput
        {
            get => _filenameToCheckInput;
            set
            {
                if (_filenameToCheckInput.Equals(value))
                {
                    return;
                }
                _filenameToCheckInput = value;
                RaisePropertyChanged(nameof(_filenameToCheckInput));
            }
        }

        private string _filenameChecksumInput;

        public string FilenameChecksumInput
        {
            get => _filenameChecksumInput;
            set
            {
                if (_filenameChecksumInput.Equals(value))
                {
                    return;
                }
                _filenameChecksumInput = value;
                RaisePropertyChanged(nameof(_filenameChecksumInput));
            }
        }

        private string _output;

        public string Output
        {
            get => _output;
            set
            {
                if (_output.Equals(value))
                {
                    return;
                }
                _output = value;
                RaisePropertyChanged(nameof(_output));
            }
        }

        private int _progressDone;

        public int ProgressDone
        {
            get => _progressDone;
            set
            {
                if (_progressDone.Equals(value))
                {
                    return;
                }
                _progressDone = value;
                RaisePropertyChanged(nameof(_progressDone));
            }
        }

        private int _progressMaximum;

        public int ProgressMaximum
        {
            get => _progressMaximum;
            set
            {
                if (_progressMaximum.Equals(value))
                {
                    return;
                }
                _progressMaximum = value;
                RaisePropertyChanged(nameof(_progressMaximum));
            }
        }

        public bool IsInProggress
        {
            get => ProgressDone != ProgressMaximum;
        }
    }
}
