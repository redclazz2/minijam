using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace minijam.Interfaces.Mediator
{
    public interface IMediator
    {
        public void Notify(string sender);
    }
}