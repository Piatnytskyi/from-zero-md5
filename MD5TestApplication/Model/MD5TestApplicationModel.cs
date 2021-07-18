namespace MD5TestApplication.Model
{
    public class MD5TestApplicationModel : AbstractModel
    {
        private string _enteredInput = string.Empty;

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
                RaisePropertyChanged(nameof(EnteredInput));
            }
        }

        private string _filenameInput = string.Empty;

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
                RaisePropertyChanged(nameof(FilenameInput));
            }
        }

        private string _filenameToCheckInput = string.Empty;

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
                RaisePropertyChanged(nameof(FilenameToCheckInput));
            }
        }

        private string _filenameChecksumInput = string.Empty;

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
                RaisePropertyChanged(nameof(FilenameChecksumInput));
            }
        }

        private string _status = string.Empty;

        public string Status
        {
            get => _status;
            set
            {
                if (_status.Equals(value))
                {
                    return;
                }
                _status = value;
                RaisePropertyChanged(nameof(Status));
            }
        }

        private string _output = string.Empty;

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
                RaisePropertyChanged(nameof(Output));
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
                RaisePropertyChanged(nameof(ProgressDone));
                RaisePropertyChanged(nameof(IsHashingInProgress));
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
                RaisePropertyChanged(nameof(ProgressMaximum));
                RaisePropertyChanged(nameof(IsHashingInProgress));
            }
        }

        public bool IsHashingInProgress
        {
            get => ProgressDone != ProgressMaximum;
        }
    }
}
