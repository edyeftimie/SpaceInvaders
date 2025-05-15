namespace SpaceInvaders {
    public interface IValidator<T> {
        bool IsValid(T obj);
        string ErrorMessage { get; }
    }
}