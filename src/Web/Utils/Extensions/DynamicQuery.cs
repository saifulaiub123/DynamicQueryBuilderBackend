using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;

//To use Dynamic Linq
using System.Linq.Dynamic.Core;
using Castle.DynamicLinqQueryBuilder;
using System.Collections.Generic;

namespace Involys.Poc.Api.Utils.Extensions
{
    public static class DynamicQuery
    {
        public static IQueryable<TSource> DynamicFilter<TSource>([NotNullAttribute] this IQueryable<TSource> source)
        {
            ParameterExpression pe = Expression.Parameter(typeof(TSource), "c");

            Expression left = Expression.PropertyOrField(pe, "Statut");
            Expression right = Expression.Constant("1");
            Expression e1 = Expression.Equal(left, right);

            return source.Where(Expression.Lambda<Func<TSource, bool>>(e1, new ParameterExpression[] { pe }));
        }


        public static IQueryable<TSource> DynamicFilter<TSource>([NotNullAttribute] this IQueryable<TSource> source, string query, params object[] args)
        {
            if(query == "Filter_Point")
            {
                return source.Where("Id > @0 and Fournisseur.Type == @1", 0, "AA").Where("Lignes.Count == 0");
            }
            return source.Where(query, args);
        }

        public static IQueryable<TSource> DynamicFilterSolution3<TSource>([NotNullAttribute] this IQueryable<TSource> source, string query, params object[] args)
        {
            // https://github.com/castle-it/dynamic-linq-query-builder
            // https://github.com/zebzhao/Angular-QueryBuilder

            var myFilterSample = new QueryBuilderFilterRule()
            {
                Condition = "and",
                Rules = new List<QueryBuilderFilterRule>()
                {
                    new QueryBuilderFilterRule()
                    {
                        Condition = "or",
                        Field = "Fournisseur.Type",
                        Id = "Fournisseur.Type",
                        Input = "NA",
                        Operator = "equal",
                        Type = "string",
                        Value = new [] { "BB" }
                    }
                }
            };


            var myFilter = new QueryBuilderFilterRule()
            {
                Condition = "and",
                Rules = new List<QueryBuilderFilterRule>()
                {
                    new QueryBuilderFilterRule()
                    {
                        Condition = "or",
                        Field = "Lignes.Montant",
                        Id = "Lignes.Montant",
                        Input = "NA",
                        Operator = "equal",
                        Type = "double",
                        Value = new [] { "1000" }
                    }
                }
            };


            
            return source.BuildQuery<TSource>(myFilter);
        }

        
    }


}

