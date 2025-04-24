using StudentPlanerApp.Models;
using StudentPlanerApp.Services;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace StudentPlanerApp.Pages
{
    public partial class ZadaciPage : ContentPage
    {
        private ZadatakService _zadatakService = new ZadatakService();
        public ObservableCollection<Zadatak> Zadaci { get; set; }

        public ZadaciPage()
        {
            InitializeComponent();
            Zadaci = new ObservableCollection<Zadatak>();
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _zadatakService.Init(); // Osiguraj da je baza inicijalizovana
            var lista = await _zadatakService.GetZadaci();
            Zadaci.Clear();
            foreach (var z in lista)
                Zadaci.Add(z);
        }

        private async void DodajZadatak_Clicked(object sender, EventArgs e)
        {
            var noviZadatak = new Zadatak
            {
                Naslov = unosNaslova.Text,
                Opis = unosOpisa.Text,
                Datum = datumPicker.Date,
                Zavrsen = false
            };

            await _zadatakService.DodajZadatak(noviZadatak);
            Zadaci.Add(noviZadatak);
            ClearForm();
        }

        private async void ObrisiZadatak_Clicked(object sender, EventArgs e)
        {
            var dugme = sender as Button;
            var zadatak = dugme?.BindingContext as Zadatak;

            if (zadatak != null)
            {
                await _zadatakService.ObrisiZadatak(zadatak);
                Zadaci.Remove(zadatak);
            }
        }

        private async void OznaciZavrsen_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var checkbox = sender as CheckBox;
            var zadatak = checkbox?.BindingContext as Zadatak;

            if (zadatak != null)
            {
                zadatak.Zavrsen = e.Value;
                await _zadatakService.IzmeniZadatak(zadatak);
            }
        }

        private void ClearForm()
        {
            unosNaslova.Text = "";
            unosOpisa.Text = "";
            datumPicker.Date = DateTime.Now;
        }
    }
}