using System;
using System.Collections.Generic;

/// <summary>
/// a collection of fabric exemplars paramerized with child classes of Tproduction
/// </summary>

public abstract class AbstractInstantiationService<TProduction>
{
    protected Dictionary<Type, IFactory<TProduction>> _factories;

    protected AbstractInstantiationService() => 
        _factories = new();

    public abstract TReturnable ReturnObject<TReturnable>() where TReturnable : TProduction;


}