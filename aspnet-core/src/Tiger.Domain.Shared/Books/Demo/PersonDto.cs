/**
 * 类    名：PersonDto   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/16 12:51:24       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Books.Demo
{
    public class PersonDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }
    }
}
