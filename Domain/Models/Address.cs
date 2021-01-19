﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Hogwarts.Domain.Models
{
    class Address
    {
      

            public int Id { get; protected set; }
            public string Street { get; protected set; }
            public string PostCode { get; protected set; }
            public string City { get; protected set; }

            public Student Student { get; protected set; }
            public int StudentId { get; protected set; }


            public Address()
            {

            }

            public Address(string street, string postCode, string city)
            {
                Street = street;
                PostCode = postCode;
                City = city;

            }

        

    }
}
