using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPF_Notepad.Model
{
    internal class Note__Model : INotifyPropertyChanged
    {
        private string title;
        private string text;
        private DateTime date;

        public Note__Model(string _title, string _text, DateTime _date)
        {
            this.title = _title;
            this.text = _text;
            this.date = _date;
        }

        public Note__Model(Note__Model selectedNote) : this(selectedNote.title, selectedNote.text, selectedNote.date) { }

        public Note__Model() : this("title", String.Empty, DateTime.Now) { }

        public string Title
        {
            get { return title; }
            set { title = value; OnPropertyChanged("Title"); }
        }

        public string Text 
        {
            get { return text; }
            set { text = value; OnPropertyChanged("Text"); }
        }
        public DateTime Date 
        {
            get { return date; }
            set { date = value; OnPropertyChanged("Date"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public override string ToString()
        {
            return $"{Text}\n{Date}";
        }
    }
}
