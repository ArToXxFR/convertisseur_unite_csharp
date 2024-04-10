using System;
using System.Data.SQLite;
using Moq;

namespace ConvertisseurUnite.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void AjouterUnIngredientDansBDD_IngredientValide_AjouteIngredient()
    {
        var gestionnaire = new GestionnaireBaseDeDonnees();
        var id = 1;
        var nom = "Eau déminéralisée";
        var masse_volumique = 1.0;

        gestionnaire.AjouterIngredient(id, nom, masse_volumique);

        Assert.True(gestionnaire.Ingredients.Any(i => i.Id == id));
    }

    [Test]
    public void AjouterUnIngredient_IngredientInvalide_RetourneErreur()
    {
        var gestionnaire = new GestionnaireBaseDeDonnees();
        var id = 1;
        var nom = "Eau déminéralisée";
        var masse_volumique = -1.0;

        Assert.Throws<ArgumentException>(() => gestionnaire.AjouterIngredient(id, nom, masse_volumique));
    }

    [Test]
    public void VerifieSiPeutRetirerUnIngredient_IngredientExiste_RetireIngredient()
    {
        var gestionnaire = new GestionnaireBaseDeDonnees();

        var id = 1;
        var nom = "Eau déminéralisée";
        var masse_volumique = 1.0;
        gestionnaire.AjouterIngredient(id, nom, masse_volumique);

        gestionnaire.RetirerIngredient(id);

        Assert.False(gestionnaire.Ingredients.Any(i => i.Id == id));
    }

    [Test]
    public void VerifieSiPeutRetirerUnIngredient_IngredientNExistePas_RetourneErreur()
    {
        var gestionnaire = new GestionnaireBaseDeDonnees();

        var id = 100;

        Assert.Throws<ArgumentException>(() => gestionnaire.RetirerIngredient(id));
    }

    [Test]
    public void ConvertirIngredient_RetourneNombreCorrect()
    {
        var gestionnaire = new GestionnaireBaseDeDonnees();

        var idIngredient = 1;
        var nom = "Eau déminéralisée";
        var masse_volumique = 1.0;
        var masse = 100.0;

        gestionnaire.AjouterIngredient(idIngredient, nom, masse_volumique);

        var convertisseur = new ConvertisseurMasseMl(gestionnaire);

        var volumeMl = convertisseur.ConvertirMasseEnMl(idIngredient, masse);

        Assert.That(volumeMl, Is.EqualTo(masse));
    }

    [Test]
    public void ConvertirIngredient_IngredientNonTrouve_RetourneException()
    {
        var idIngredientInexistant = 100;
        var masse = 100.0;

        var gestionnaire = new GestionnaireBaseDeDonnees();
        var convertisseur = new ConvertisseurMasseMl(gestionnaire);

        Assert.Throws<ArgumentException>(() => convertisseur.ConvertirMasseEnMl(idIngredientInexistant, masse));
    }

    [Test]
    public void AjouterElementHistorique_IngredientValide_AjouteElementHistorique()
    {
            var idIngredient = 1;
            var nomIngredient = "Eau déminéralisée";
            var masse_volumique = 1.0;
            var masse = 100.0;

            var gestionnaire = new GestionnaireBaseDeDonnees();
            gestionnaire.AjouterIngredient(idIngredient, nomIngredient, masse_volumique);

            var convertisseur = new ConvertisseurMasseMl(gestionnaire);
            convertisseur.ConvertirMasseEnMl(idIngredient, masse);

            Assert.That(gestionnaire.Historiques.Count, Is.EqualTo(1));
    }
}