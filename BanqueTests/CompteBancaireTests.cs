using Microsoft.VisualStudio.TestTools.UnitTesting;
using BanqueCamara;
using System;

namespace BanqueTests
{
    [TestClass]
    public class CompteBancaireTests
    {
        [TestMethod]
        public void Crediter_MontantPositif_AugmenteSolde()
        {
            // Arrange
            var compte = new CompteBancaire("Client Test", 1000);
            decimal montant = 500;

            // Act
            compte.Crediter(montant);

            // Assert
            Assert.AreEqual(1500, compte.Solde);
        }

        [TestMethod]
        public void Crediter_MontantNegatif_DevraitLeverException()
        {
            // Arrange
            var compte = new CompteBancaire("Client Test", 1000);

            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => compte.Crediter(-100));
        }

        [TestMethod]
        public void Debiter_MontantValide_DiminueSolde()
        {
            // Arrange
            var compte = new CompteBancaire("Client Test", 1000);
            decimal montant = 300;

            // Act
            compte.Debiter(montant);

            // Assert
            Assert.AreEqual(700, compte.Solde);
        }

        [TestMethod]
        public void Debiter_MontantNegatif_DevraitLeverException()
        {
            // Arrange
            var compte = new CompteBancaire("Client Test", 1000);

            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => compte.Debiter(-100));
        }

        [TestMethod]
        public void Debiter_MontantSuperieurAuSolde_DevraitLeverException()
        {
            // Arrange
            var compte = new CompteBancaire("Client Test", 500);

            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => compte.Debiter(600));
        }

        [TestMethod]
        public void Constructeur_NomValide_CreerCompte()
        {
            // Arrange & Act
            var compte = new CompteBancaire("Jean Dupont", 1000);

            // Assert
            Assert.AreEqual("Jean Dupont", compte.NomClient);
            Assert.AreEqual(1000, compte.Solde);
        }

        [TestMethod]
        public void Constructeur_NomVide_DevraitLeverException()
        {
            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => new CompteBancaire("", 1000));
        }

        [TestMethod]
        public void Constructeur_NomNull_DevraitLeverException()
        {
            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => new CompteBancaire(null, 1000));
        }

        [TestMethod]
        public void Constructeur_SoldeNegatif_DevraitLeverException()
        {
            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new CompteBancaire("Client", -100));
        }

        [TestMethod]
        public void Crediter_PlusieursOperations_MettentAJourSolde()
        {
            // Arrange
            var compte = new CompteBancaire("Client Test", 1000);

            // Act
            compte.Crediter(200);
            compte.Crediter(300);
            compte.Crediter(500);

            // Assert
            Assert.AreEqual(2000, compte.Solde);
        }

        [TestMethod]
        public void Debiter_PlusieursOperations_MettentAJourSolde()
        {
            // Arrange
            var compte = new CompteBancaire("Client Test", 1000);

            // Act
            compte.Debiter(100);
            compte.Debiter(200);
            compte.Debiter(150);

            // Assert
            Assert.AreEqual(550, compte.Solde);
        }

        [TestMethod]
        public void OperationsMixtes_CrediterEtDebiter_MettentAJourSolde()
        {
            // Arrange
            var compte = new CompteBancaire("Client Test", 1000);

            // Act
            compte.Crediter(500);
            compte.Debiter(300);
            compte.Crediter(200);
            compte.Debiter(100);

            // Assert
            Assert.AreEqual(1300, compte.Solde);
        }

        [TestMethod]
        public void Debiter_SoldeExactementEgalAuMontant_VideSolde()
        {
            // Arrange
            var compte = new CompteBancaire("Client Test", 500);

            // Act
            compte.Debiter(500);

            // Assert
            Assert.AreEqual(0, compte.Solde);
        }

        [TestMethod]
        public void Crediter_MontantZero_DevraitLeverException()
        {
            // Arrange
            var compte = new CompteBancaire("Client Test", 1000);

            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => compte.Crediter(0));
        }

        [TestMethod]
        public void Debiter_MontantZero_DevraitLeverException()
        {
            // Arrange
            var compte = new CompteBancaire("Client Test", 1000);

            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => compte.Debiter(0));
        }

        [TestMethod]
        public void Constructeur_SoldeInitialZero_CreerCompte()
        {
            // Arrange & Act
            var compte = new CompteBancaire("Client Test", 0);

            // Assert
            Assert.AreEqual(0, compte.Solde);
            Assert.AreEqual("Client Test", compte.NomClient);
        }

        [TestMethod]
        public void NomClient_ProprieteEnLectureSeule_RetourneNomCorrect()
        {
            // Arrange & Act
            var compte = new CompteBancaire("Marie Martin", 2000);

            // Assert
            Assert.AreEqual("Marie Martin", compte.NomClient);
        }

        [TestMethod]
        public void Crediter_MontantDecimal_AugmenteSoldePrecisement()
        {
            // Arrange
            var compte = new CompteBancaire("Client Test", 1000.50m);

            // Act
            compte.Crediter(250.75m);

            // Assert
            Assert.AreEqual(1251.25m, compte.Solde);
        }

        [TestMethod]
        public void Debiter_MontantDecimal_DiminueSoldePrecisement()
        {
            // Arrange
            var compte = new CompteBancaire("Client Test", 1000.99m);

            // Act
            compte.Debiter(500.49m);

            // Assert
            Assert.AreEqual(500.50m, compte.Solde);
        }
    }
}