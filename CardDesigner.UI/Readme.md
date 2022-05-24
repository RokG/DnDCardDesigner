# Markdown File

# Adding Views and ViewModels
- View: Create a new UserControl in View folder
--  Nothing special has to be done in either ViewModel.xaml or ViewModel.xaml.cs
- ViewModel: Create a new Class in ViewModel folder
-- Use ViewModelBase in class definition
-- Crate a LoadModel method, pass store to constructor and create Name in constructor
- HostBuilder:
-- AddNavigationServiceExtension: register navigation service, take other veiw models as example
-- AddViewModelsExtension: Add Transient, Singleton and static method, like for existing ViewModels