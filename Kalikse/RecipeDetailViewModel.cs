using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Kalikse
{
    public class RecipeDetailViewModel : INotifyPropertyChanged
    {
        private string userBudget;
        public string UserBudget
        {
            get => userBudget;
            set
            {
                if (userBudget != value)
                {
                    userBudget = value;
                    OnPropertyChanged();
                }
            }
        }

        private string userDietPreference;
        public string UserDietPreference
        {
            get => userDietPreference;
            set
            {
                if (userDietPreference != value)
                {
                    userDietPreference = value;
                    OnPropertyChanged();
                }
            }
        }

        private string userAllergens;
        public string UserAllergens
        {
            get => userAllergens;
            set
            {
                if (userAllergens != value)
                {
                    userAllergens = value;
                    OnPropertyChanged();
                }
            }
        }

        private string image;
        public string Image
        {
            get => image;
            set
            {
                if (image != value)
                {
                    image = value;
                    OnPropertyChanged();
                }
            }
        }

        private string name;
        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<string> dietaryTags = new ObservableCollection<string>();
        public ObservableCollection<string> DietaryTags
        {
            get => dietaryTags;
            set
            {
                if (dietaryTags != value)
                {
                    dietaryTags = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<string> ingredients = new ObservableCollection<string>();
        public ObservableCollection<string> Ingredients
        {
            get => ingredients;
            set
            {
                if (ingredients != value)
                {
                    ingredients = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<InstructionStep> steps = new ObservableCollection<InstructionStep>();
        public ObservableCollection<InstructionStep> Steps
        {
            get => steps;
            set
            {
                if (steps != value)
                {
                    steps = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public class InstructionStep
    {
        public int Number { get; set; }
        public string Text { get; set; }
    }
}
