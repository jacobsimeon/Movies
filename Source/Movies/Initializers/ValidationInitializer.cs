namespace Movies.Initializers
{
    using System;

    using FluentValidation;

    using StructureMap;

    public class ValidationInitializer : ValidatorFactoryBase
    {
        public override IValidator CreateInstance(Type validatorType)
        {
            return ObjectFactory.GetInstance(validatorType) as IValidator;
        }
    }
}