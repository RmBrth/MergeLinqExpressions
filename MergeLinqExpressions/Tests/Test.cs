using MergeLinqExpressions.Data;
using MergeLinqExpressions.Helpers;
using MergeLinqExpressions.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace MergeLinqExpressions.Tests
{
    public class Test
    {
        [Fact]
        public void GetCountries()
        {
            var service = new CountryService();
            var countries = service.GetCountries();
            Assert.NotNull(countries);
        }

        [Fact]
        public void FilterCountriesWithMergedExpression()
        {
            var service = new CountryService();
            var countries = service.GetCountries();

            Expression<Func<Country, bool>> expressionFR = x => x.Code == "FR";
            var result = countries.Where(expressionFR.Compile());
            Assert.Single(result);

            Expression<Func<Country, bool>> expressionGB = x => x.Code == "GB";
            result = countries.Where(expressionGB.Compile());
            Assert.Single(result);

            Expression<Func<Country, bool>> expressionFRorUK = expressionFR.Merge(expressionGB, Expression.OrElse);
            result = countries.Where(expressionFRorUK.Compile());
            Assert.Equal(2, result.Count());
        }
    }
}
