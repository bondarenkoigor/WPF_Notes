using GalaSoft.MvvmLight.Command;
using System.IO;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WPF_Notepad.Model;
using System;
using System.Linq;
using System.Windows.Controls;

namespace WPF_Notepad.ViewModel
{
    internal class MainWindow__ViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Note__Model> notes;

        public ObservableCollection<Note__Model> Notes
        {
            get { return FilterNotes(); }
            set { notes = value; }
        }

        private string filter = String.Empty;

        public string Filter
        {
            get { return filter; }
            set { filter = value; OnPropertyChanged("Notes"); }
        }

        private bool sortByTitle;

        public bool SortByTitle
        {
            get { return sortByTitle; }
            set { sortByTitle = value; OnPropertyChanged("Notes"); }
        }


        private Note__Model selectedNote;

        public Note__Model SelectedNote
        {
            get { return selectedNote; }
            set { selectedNote = value; OnPropertyChanged("SelectedNote"); }
        }

        public MainWindow__ViewModel()
        {
            Notes = new ObservableCollection<Note__Model>();
            this.Read();
        }


        private RelayCommand addCommand;

        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ?? (addCommand = new RelayCommand(() =>
        {
            Note__Model note = new Note__Model();
            Notes.Add(note);
            SelectedNote = note;
        }));
            }
            set { addCommand = value; }
        }

        private RelayCommand deleteCommand;

        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ?? (deleteCommand = new RelayCommand(() =>
                {
                    Notes.Remove(SelectedNote);
                }));
            }
            set { deleteCommand = value; }
        }

        private RelayCommand sortCommand;

        public RelayCommand SortCommand
        {
            get
            {
                return sortCommand ?? (sortCommand = new RelayCommand(() =>
                {
                    this.SortByTitle = !this.sortByTitle;
                }));
            }
            set { sortCommand = value; }
        }


        public ObservableCollection<Note__Model> FilterNotes()
        {
            var filteredNotes = new ObservableCollection<Note__Model>();
            if (this.filter != String.Empty)
                foreach (var note in this.notes)
                {
                    if (note.Title.Contains(this.filter)) filteredNotes.Add(note);
                }
            else filteredNotes = this.notes;

            if (sortByTitle)
                filteredNotes = new ObservableCollection<Note__Model>(filteredNotes.OrderBy(note => note.Title));

            return filteredNotes;
        }



        public void Save()
        {
            File.WriteAllText("SaveFile.txt", "");
            foreach (var note in this.notes)
            {
                File.AppendAllText("SaveFile.txt", $"{note.Title}\t{note.Text}\t{note.Date}\n");
            }
        }

        public void Read()
        {
            if (!File.Exists("SaveFile.txt")) return;

            foreach (string line in File.ReadAllLines("SaveFile.txt"))
            {
                string[] note = line.Split('\t');
                this.notes.Add(new Note__Model(note[0], note[1], Convert.ToDateTime(note[2])));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;


        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
