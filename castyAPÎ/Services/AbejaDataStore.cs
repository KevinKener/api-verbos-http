using castyAPÎ.Models;

namespace castyAPÎ.Services; //Name space esta bueno que agrupe cosas que tengan que ver entre si no para todo. 


public class AbejaDataStore
{
    //Vamos a guardar en memoria una lista de Abejas:
    public List<Abeja> Abejas { get; set; } // property

   //Importante implementar patron llamado singelton

   //Lo llamo directamente, es de solo lectura y va a tener 1 objeto nuevod e AbejaDataStore.  Asi te aseguras que hay solo una instancia de data store.
    public static  AbejaDataStore Current { get; } = new AbejaDataStore(); //Propiedad Estatica llamado AbejaDataStore, propeiedad estatica no se necesita crear objetio de la clase.

    //Constructor:
    //le voy a poner todas las abejas que quiero poner. Base de datos data store- Para no crear base de datos, se puede usar una Base de datos No sql.
    public AbejaDataStore()
    {
        Abejas = new List<Abeja>() {
            new Abeja() {
                Id = 1,
                Nombre = "Queen Abeja Luli",
                Apellido = "Amadio",
                Habilidades = new List<Habilidad>() {
                    new Habilidad() {
                        Id = 1,
                        Nombre = "Golpe de Miel",
                        Potencia = Habilidad.EPotencia.Fuerte
                    }
                }
            },
             new Abeja() {
                Id = 2,
                Nombre = "King Abeja Kevin",
                Apellido = "Kener",
                Habilidades = new List<Habilidad>() {
                    new Habilidad() {
                        Id = 1,
                        Nombre = "Aguijonazo Envenenado",
                        Potencia = Habilidad.EPotencia.Extremo
                    },
                    new Habilidad() {
                        Id = 2,
                        Nombre = "Mordida Letal",
                        Potencia = Habilidad.EPotencia.Moderado
                    }

                }
            },
             new Abeja() {
                Id = 3,
                Nombre = "Abeja Pinky",
                Apellido = "Kener-Amadio",
                Habilidades = new List<Habilidad>() {
                    new Habilidad() {
                        Id = 1,
                        Nombre = "Patada voladora",
                        Potencia = Habilidad.EPotencia.Suave
                    },
                    new Habilidad() {
                        Id = 2,
                        Nombre = "Espinas voladoras",
                        Potencia = Habilidad.EPotencia.Extremo
                    },
                    new Habilidad() {
                        Id = 3,
                        Nombre = "Zumbido Aturdidor",
                        Potencia = Habilidad.EPotencia.Fuerte
                    }
                }
            },

        };

    }

}
