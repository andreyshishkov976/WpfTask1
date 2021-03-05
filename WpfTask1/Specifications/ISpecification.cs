namespace WpfTask1.Specifications
{
    interface ISpecification<T>
    {
        bool IsSatisfiedBy(T candidate);
        Specification<T> And(Specification<T> other);
        //Specification<T> AndNot(Specification<T> other);
        //Specification<T> Or(Specification<T> other);
        //Specification<T> OrNot(Specification<T> other);
        //Specification<T> Not();
    }
}
