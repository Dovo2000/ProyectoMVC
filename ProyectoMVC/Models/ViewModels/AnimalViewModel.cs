namespace ProyectoMVC.Models.ViewModels
{
    public class AnimalViewModel
    {
        public List<string> Animals { get; set; } = new List<string>();

        public AnimalViewModel() 
        {
            Animals = new List<string>
            {
                "Elefante",
                "Tigre",
                "León",
                "Jirafa",
                "Rinoceronte",
                "Oso Panda",
                "Koala",
                "Cephalopodo",
                "Canguro",
                "Águila"
            };
        }
    }
}
