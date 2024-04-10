namespace ConvertisseurUnite {
    public class ConvertisseurMasseMl
    {
        public GestionnaireBaseDeDonnees _gestionnaireBaseDeDonnees;

        public ConvertisseurMasseMl(GestionnaireBaseDeDonnees gestionnaireBaseDeDonnees)
        {
            _gestionnaireBaseDeDonnees = gestionnaireBaseDeDonnees;
        }

        public double ConvertirMasseEnMl(int idIngredient, double masse)
        {
            var ingredient = _gestionnaireBaseDeDonnees.Ingredients.Find(i => i.Id == idIngredient);

            if (ingredient == null)
            {
                throw new ArgumentException($"L'ingrédient avec l'ID {idIngredient} n'existe pas");
            }

            var volumeMl = masse / ingredient.Masse_Volumique;
            _gestionnaireBaseDeDonnees.AjouterElementHistorique(ingredient, volumeMl);
            return volumeMl;
        }
    }
}