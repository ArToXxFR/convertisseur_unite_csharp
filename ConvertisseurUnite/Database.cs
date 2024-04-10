public sealed class GestionnaireBaseDeDonnees
{
    public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    public List<Historique> Historiques { get; set; } = new List<Historique>();

    public GestionnaireBaseDeDonnees()
    {

    }

    public void AjouterIngredient(int id, string nom, double masse_volumique)
    {
        if (id <= 0)
        {
            throw new ArgumentException("L'ID de l'ingrédient doit être positif");
        }

        if (string.IsNullOrEmpty(nom))
        {
            throw new ArgumentException("Le nom de l'ingrédient ne peut pas être vide");
        }

        if (masse_volumique <= 0)
        {
            throw new ArgumentException("La masse volumique de l'ingrédient doit être positive");
        }

        Ingredients.Add(new Ingredient(id, nom, masse_volumique));
    }

    public void RetirerIngredient(int id)
    {
         var ingredient = Ingredients.Find(i => i.Id == id);

        if (ingredient == null)
        {
            throw new ArgumentException($"L'ingrédient avec l'ID {id} n'existe pas");
        }

        Ingredients.Remove(ingredient);
    }
    
    public void AjouterElementHistorique(Ingredient ingredient, double resultat)
    {
        var historique = new Historique(
            Historiques.Count + 1,
            ingredient,
            resultat
        );

        Historiques.Add(historique);
    }

    public class Ingredient
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public double Masse_Volumique { get; set; }

        public Ingredient(int id, string nom, double masse_volumique)
        {
            Id = id;
            Nom = nom;
            Masse_Volumique = masse_volumique;
        }
        
    }

    public class Historique
    {
        public int Id { get; set; }
        public Ingredient? Ingredient { get; set; }
        public double Resultat { get; set; }

        public Historique(int id, Ingredient ingredient, double resultat)
        {
            Id = id;
            Ingredient = ingredient;
            Resultat = resultat;
        }
    }
}

