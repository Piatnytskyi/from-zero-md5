using MD5TestApplication.Command;
using MD5TestApplication.Model;

namespace MD5TestApplication.ViewModel
{
    class MD5TestApplicationViewModel
    {
        public MD5TestApplicationModel MD5TestApplicationModel { get; set; } = new MD5TestApplicationModel();

        public RelayCommand SaveAsCommand { get; set; }

        public RelayCommand HashCommand { get; set; }

        public RelayCommand ChooseFileToHashCommand { get; set; }
        public RelayCommand HashFileCommand { get; set; }

        public RelayCommand ChooseFileAndChecksumCommand { get; set; }
        public RelayCommand CompareFileHashAndChecksumCommand { get; set; }

        public MD5TestApplicationViewModel()
        {
            SaveAsCommand = new RelayCommand(o => SaveAs(), c => CanSaveAs());

            HashCommand = new RelayCommand(o => Hash(), c => CanHash());

            ChooseFileToHashCommand = new RelayCommand(o => ChooseFileToHash(), c => CanChooseFileToHash());
            HashFileCommand = new RelayCommand(o => HashFile(), c => CanHashFile());

            ChooseFileAndChecksumCommand = new RelayCommand(o => ChooseFileAndChecksum(), c => CanChooseFileAndChecksum());
            CompareFileHashAndChecksumCommand = new RelayCommand(o => CompareFileHashAndChecksum(), c => CanCompareFileHashAndChecksum());
        }

        private void SaveAs()
        {

        }

        private bool CanSaveAs()
        {
            return !string.IsNullOrEmpty(MD5TestApplicationModel.Output);
        }

        private void Hash()
        {
            
        }

        private bool CanHash()
        {
            return false;
        }

        private void ChooseFileToHash()
        {

        }

        private bool CanChooseFileToHash()
        {
            return false;
        }

        private void HashFile()
        {

        }

        private bool CanHashFile()
        {
            return false;
        }

        private void ChooseFileAndChecksum()
        {
            
        }

        private bool CanChooseFileAndChecksum()
        {
            return false;
        }

        private void CompareFileHashAndChecksum()
        {

        }

        private bool CanCompareFileHashAndChecksum()
        {
            return false;
        }
    }
}
