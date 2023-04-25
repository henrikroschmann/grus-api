﻿using HotChocolate.Types;

namespace Grus.Domain.Entities.Budget;

public class Income
{
    public string Id { get; set; }
    public string Source { get; set; }
    public double Amount { get; set; }
}

public class IncomeType : ObjectType<Income>
{
    protected override void Configure(IObjectTypeDescriptor<Income> descriptor)
    {
        descriptor.Field(t => t.Id).Type<NonNullType<IdType>>();
        descriptor.Field(t => t.Source).Type<StringType>();
        descriptor.Field(t => t.Amount).Type<FloatType>();
    }
}