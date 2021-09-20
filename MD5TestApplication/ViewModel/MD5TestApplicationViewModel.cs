using MD5;
using MD5TestApplication.Commands;
using Microsoft.Win32;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MD5TestApplication.ViewModel
{
    class MD5TestApplicationViewModel : AbstractViewModel
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

        public RelayCommand SaveAsCommand { get; set; }

        public RelayCommand HashCommand { get; set; }

        public RelayCommand ChooseFileToHashCommand { get; set; }
        public AsyncCommand HashFileCommand { get; set; }

        public RelayCommand ChooseFileAndChecksumCommand { get; set; }
        public AsyncCommand CompareFileHashAndChecksumCommand { get; set; }

        public MD5TestApplicationViewModel()
        {
            SaveAsCommand = new RelayCommand(o => SaveAs(), c => CanSaveAs());

            HashCommand = new RelayCommand(o => Hash(), c => CanHash());

            ChooseFileToHashCommand = new RelayCommand(o => ChooseFileToHash(), c => CanChooseFileToHash());
            HashFileCommand = new AsyncCommand(o => HashFile(), c => CanHashFile());

            ChooseFileAndChecksumCommand = new RelayCommand(o => ChooseFileAndChecksum(), c => CanChooseFileAndChecksum());
            CompareFileHashAndChecksumCommand = new AsyncCommand(o => CompareFileHashAndChecksum(), c => CanCompareFileHashAndChecksum());
        }

        private void SaveAs()
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = FilenameInput + "Hash";
            saveFileDialog.Filter = "Binary files (*.dat;*.bin)|*.dat;*.bin";

            if (saveFileDialog.ShowDialog() == true)
            {
                using BinaryWriter writer = new BinaryWriter(File.Open(saveFileDialog.FileName, FileMode.Create));
                    writer.Write(Output);
            }
        }

        private bool CanSaveAs()
        {
            return !string.IsNullOrEmpty(Output);
        }

        private void UpdateHashingProgress(object sender, HashingProgressEventArgs hashingProgressEventArgs)
        {
            ProgressDone = hashingProgressEventArgs.Done;
            ProgressMaximum = hashingProgressEventArgs.OutOf;
        }

        private void Hash()
        {
            var mD5 = new MD5.MD5();
            mD5.HashingProgressChanged += UpdateHashingProgress;

            Output = mD5.ComputeHash(Encoding.Default.GetBytes(EnteredInput ?? string.Empty)).ToString();
            Status = "Result of hashing \"" + EnteredInput + "\":";

            mD5.HashingProgressChanged -= UpdateHashingProgress;
        }

        private bool CanHash()
        {
            return !IsHashingInProgress;
        }

        private void ChooseFileToHash()
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Choose File to Hash";
            openFileDialog.Filter = "All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
                FilenameInput = openFileDialog.FileName;
        }

        private bool CanChooseFileToHash()
        {
            return !IsHashingInProgress;
        }

        private async Task HashFile()
        {
            var mD5 = new MD5.MD5();
            mD5.HashingProgressChanged += UpdateHashingProgress;

            Status = "Hashing of \"" + FilenameInput + "\" in progress:";
            Output = (await mD5.ComputeHash(File.OpenRead(FilenameInput))).ToString();
            Status = "Result of hashing \"" + FilenameInput + "\":";

            mD5.HashingProgressChanged -= UpdateHashingProgress;
        }

        private bool CanHashFile()
        {
            return !IsHashingInProgress
                && !string.IsNullOrEmpty(FilenameInput);
        }

        private void ChooseFileAndChecksum()
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Choose File to Check";
            openFileDialog.Filter = "All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
                FilenameToCheckInput = openFileDialog.FileName;

            var openChecksumDialog = new OpenFileDialog();
            openFileDialog.Title = "Choose Checksum";
            openChecksumDialog.Filter = "Binary files (*.dat;*.bin)|*.dat;*.bin";

            if (openChecksumDialog.ShowDialog() == true)
                FilenameChecksumInput = openChecksumDialog.FileName;
        }

        private bool CanChooseFileAndChecksum()
        {
            return !IsHashingInProgress;
        }

        private async Task CompareFileHashAndChecksum()
        {
            var mD5 = new MD5.MD5();
            mD5.HashingProgressChanged += UpdateHashingProgress;

            Status = "Hashing of \"" + FilenameToCheckInput + "\" in progress:";
            Output = (await mD5.ComputeHash(File.OpenRead(FilenameToCheckInput))).ToString();

            mD5.HashingProgressChanged -= UpdateHashingProgress;

            var checksum = string.Empty;
            using BinaryReader reader = new BinaryReader(File.Open(FilenameChecksumInput, FileMode.Open));
                checksum = reader.ReadString();

            if (Output.Equals(checksum, StringComparison.InvariantCultureIgnoreCase))
                Status = "File \"" + FilenameToCheckInput + "\" checksum is valid:";
            else
                Status = "File \"" + FilenameToCheckInput + "\" checksum is invalid, actual one:";
        }

        private bool CanCompareFileHashAndChecksum()
        {
            return !IsHashingInProgress
                && !string.IsNullOrEmpty(FilenameToCheckInput)
                && !string.IsNullOrEmpty(FilenameChecksumInput);
        }
    }
}
