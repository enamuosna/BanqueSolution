using System;

namespace BanqueCamara
{
    /// <summary>
    /// Classe représentant un compte bancaire
    /// </summary>
    public class CompteBancaire
    {
        private readonly string m_nomClient;
        private decimal m_solde;

        // Constructeur
        public CompteBancaire(string nomClient, decimal solde)
        {
            if (string.IsNullOrWhiteSpace(nomClient))
            {
                throw new ArgumentException("Le nom du client ne peut pas être vide.", nameof(nomClient));
            }

            if (solde < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(solde), "Le solde initial ne peut pas être négatif.");
            }

            m_nomClient = nomClient;
            m_solde = solde;
        }

        // Propriétés
        public string NomClient
        {
            get { return m_nomClient; }
        }

        public decimal Solde
        {
            get { return m_solde; }
        }

        /// <summary>
        /// Débite un montant du compte
        /// </summary>
        /// <param name="montant">Montant à débiter</param>
        public void Debiter(decimal montant)
        {
            if (montant <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(montant), "Le montant du débit doit être supérieur à zéro");
            }

            if (montant > m_solde)
            {
                throw new ArgumentOutOfRangeException(nameof(montant), "Le montant du débit est supérieur au solde");
            }

            m_solde -= montant;
        }

        /// <summary>
        /// Crédite un montant sur le compte
        /// </summary>
        /// <param name="montant">Montant à créditer</param>
        public void Crediter(decimal montant)
        {
            if (montant <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(montant), "Le montant du crédit doit être supérieur à zéro");
            }

            m_solde += montant;
        }

        /// <summary>
        /// Effectue un virement vers un autre compte
        /// </summary>
        /// <param name="montant">Montant à virer</param>
        /// <param name="compteDestination">Compte destinataire</param>
        public void Virement(decimal montant, CompteBancaire compteDestination)
        {
            if (compteDestination == null)
            {
                throw new ArgumentNullException(nameof(compteDestination), "Le compte de destination ne peut pas être null");
            }

            if (montant <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(montant), "Le montant du virement doit être supérieur à zéro");
            }

            if (montant > m_solde)
            {
                throw new ArgumentOutOfRangeException(nameof(montant), "Solde insuffisant pour effectuer le virement");
            }

            // Débiter le compte source
            this.Debiter(montant);
            
            // Créditer le compte destination
            compteDestination.Crediter(montant);
        }
    }
}