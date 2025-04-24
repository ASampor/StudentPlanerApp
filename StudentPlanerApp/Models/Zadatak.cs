using SQLite;

namespace StudentPlanerApp.Models
{
	public Class Zadatak
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }

		public string Naziv { get; set; }
	    public string Opis { get; set; }
	    public DateTime Datum { get; set; }
	    public bool Zavrsen { get; set; }



	}
}
