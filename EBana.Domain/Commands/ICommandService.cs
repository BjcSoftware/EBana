namespace EBana.Domain.Commands
{
    /// <summary>
    /// Gère l'exécution de commandes.
    /// Les commandes peuvent réaliser des traitements mais ne produisent pas de résultat.
    /// </summary>
    public interface ICommandService<TCommand>
    {
        /// <summary>
        /// Exécuter la commande.
        /// </summary>
        /// <param name="command">Données décrivant la commande à exécuter</param>
        void Execute(TCommand command);
    }
}
