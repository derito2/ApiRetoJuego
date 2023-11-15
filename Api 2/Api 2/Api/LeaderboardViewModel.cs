using System;

// Define el espacio de nombres Api, que agrupa clases relacionadas con la API.
namespace Api
{
    // La clase 'LeaderboardViewModel' representa una LeaderboardViewModel con propiedades específicas.
    public class LeaderboardViewModel
    {
        public int UserId { get; set; }// Propiedad 'UserId': Almacena un identificador único para cada usuario.
        public string Name { get; set; }// Propiedad 'Name': Almacena el nombre del leaderboard.
        public int TotalScore { get; set; }// Propiedad 'TotalScore': Almacena el puntaje.
    }
}

