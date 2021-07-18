using MD5;
using MD5TestApplication.Commands;
using MD5TestApplication.Model;
using Microsoft.Win32;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MD5TestApplication.ViewModel
{
    class MD5TestApplicationViewModel
    {
        public MD5TestApplicationModel MD5TestApplicationModel { get; set; } = new MD5TestApplicationModel();

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
            saveFileDialog.FileName = MD5TestApplicationModel.FilenameInput + "Hash";
            saveFileDialog.Filter = "Binary files (*.dat;*.bin)|*.dat;*.bin";

            if (saveFileDialog.ShowDialog() == true)
            {
                using BinaryWriter writer = new BinaryWriter(File.Open(saveFileDialog.FileName, FileMode.Create));
                    writer.Write(MD5TestApplicationModel.Output);
            }
        }

        private bool CanSaveAs()
        {
            return !string.IsNullOrEmpty(MD5TestApplicationModel.Output);
        }

        private void UpdateHashingProgress(object sender, HashingProgressEventArgs hashingProgressEventArgs)
        {
            MD5TestApplicationModel.ProgressDone = hashingProgressEventArgs.Done;
            MD5TestApplicationModel.ProgressMaximum = hashingProgressEventArgs.OutOf;
        }

        private void Hash()
        {
            var mD5 = new MD5.MD5();
            mD5.HashingProgressChanged += UpdateHashingProgress;

            MD5TestApplicationModel.Output = mD5.ComputeHash(Encoding.Default.GetBytes(MD5TestApplicationModel.EnteredInput ?? string.Empty)).ToString();
            MD5TestApplicationModel.Status = "Result of hashing \"" + MD5TestApplicationModel.EnteredInput + "\":";

            mD5.HashingProgressChanged -= UpdateHashingProgress;
        }

        private bool CanHash()
        {
            return !MD5TestApplicationModel.IsHashingInProgress;
        }

        private void ChooseFileToHash()
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Choose File to Hash";
            openFileDialog.Filter = "All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
                MD5TestApplicationModel.FilenameInput = openFileDialog.FileName;
        }

        private bool CanChooseFileToHash()
        {
            return !MD5TestApplicationModel.IsHashingInProgress;
        }

        private async Task HashFile()
        {
            var mD5 = new MD5.MD5();
            mD5.HashingProgressChanged += UpdateHashingProgress;

            MD5TestApplicationModel.Status = "Hashing of \"" + MD5TestApplicationModel.FilenameInput + "\" in progress:";
            MD5TestApplicationModel.Output = (await mD5.ComputeHash(File.OpenRead(MD5TestApplicationModel.FilenameInput))).ToString();
            MD5TestApplicationModel.Status = "Result of hashing \"" + MD5TestApplicationModel.FilenameInput + "\":";

            mD5.HashingProgressChanged -= UpdateHashingProgress;
        }

        private bool CanHashFile()
        {
            return !MD5TestApplicationModel.IsHashingInProgress
                && !string.IsNullOrEmpty(MD5TestApplicationModel.FilenameInput);
        }

        private void ChooseFileAndChecksum()
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Choose File to Check";
            openFileDialog.Filter = "All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
                MD5TestApplicationModel.FilenameToCheckInput = openFileDialog.FileName;

            var openChecksumDialog = new OpenFileDialog();
            openFileDialog.Title = "Choose Checksum";
            openChecksumDialog.Filter = "Binary files (*.dat;*.bin)|*.dat;*.bin";

            if (openChecksumDialog.ShowDialog() == true)
                MD5TestApplicationModel.FilenameChecksumInput = openChecksumDialog.FileName;
        }

        private bool CanChooseFileAndChecksum()
        {
            return !MD5TestApplicationModel.IsHashingInProgress;
        }

        private async Task CompareFileHashAndChecksum()
        {
            var mD5 = new MD5.MD5();
            mD5.HashingProgressChanged += UpdateHashingProgress;

            MD5TestApplicationModel.Status = "Hashing of \"" + MD5TestApplicationModel.FilenameToCheckInput + "\" in progress:";
            MD5TestApplicationModel.Output = (await mD5.ComputeHash(File.OpenRead(MD5TestApplicationModel.FilenameToCheckInput))).ToString();

            mD5.HashingProgressChanged -= UpdateHashingProgress;

            var checksum = string.Empty;
            using BinaryReader reader = new BinaryReader(File.Open(MD5TestApplicationModel.FilenameChecksumInput, FileMode.Open));
                checksum = reader.ReadString();

            if (MD5TestApplicationModel.Output.Equals(checksum, StringComparison.InvariantCultureIgnoreCase))
                MD5TestApplicationModel.Status = "File \"" + MD5TestApplicationModel.FilenameToCheckInput + "\" checksum is valid:";
            else
                MD5TestApplicationModel.Status = "File \"" + MD5TestApplicationModel.FilenameToCheckInput + "\" checksum is invalid, actual one:";
        }

        private bool CanCompareFileHashAndChecksum()
        {
            return !MD5TestApplicationModel.IsHashingInProgress
                && !string.IsNullOrEmpty(MD5TestApplicationModel.FilenameToCheckInput)
                && !string.IsNullOrEmpty(MD5TestApplicationModel.FilenameChecksumInput);
        }
    }
}
