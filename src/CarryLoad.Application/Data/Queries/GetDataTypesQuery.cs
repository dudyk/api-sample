using System;
using CarryLoad.Application.Data.Responses;
using MediatR;
using System.Collections.Generic;

namespace CarryLoad.Application.Data.Queries
{
    public class GetDataTypesQuery : IRequest<IEnumerable<DataTypeResult>>
    {
        public Type EnumType { get; set; }
    }
}
