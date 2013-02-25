﻿using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Filter.FilterCalls
{
	[TestFixture]
  public class FilterCallsTests : BaseJsonTests
	{
		[Test]
		public void AndFilterCombines()
		{
      var s = new SearchDescriptor<ElasticSearchProject>()
        .From(0)
        .Take(10)
        .Filter(ff=>
          ff.And(af=> 
            af.Term(f=>f.Name, "foo")
            || af.Term(f => f.Name, "bar")
          ));
			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
    [Test]
    public void AndFilterMultipleCombines()
    {
      var s = new SearchDescriptor<ElasticSearchProject>()
        .From(0)
        .Take(10)
        .Filter(ff =>
          ff.And(af =>
            af.Term(f => f.Name, "foo")
            || af.Term(f => f.Name, "bar")
            , af =>
               af.Term(f => f.Name, "foo2")
            || af.Term(f => f.Name, "bar2")
          ));
      this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
    }
    [Test]
    public void OrFilterCombines()
    {
      var s = new SearchDescriptor<ElasticSearchProject>()
        .From(0)
        .Take(10)
        .Filter(ff =>
          ff.Or(of =>
            of.Term(f => f.Name, "foo")
            && of.Term(f => f.Name, "bar")
          ));
      this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
    }
    [Test]
    public void OrFilterMultipleCombines()
    {
      var s = new SearchDescriptor<ElasticSearchProject>()
        .From(0)
        .Take(10)
        .Filter(ff =>
          ff.Or(of =>
            of.Term(f => f.Name, "foo")
            && of.Term(f => f.Name, "bar")
            , of =>
               of.Term(f => f.Name, "foo2")
            && of.Term(f => f.Name, "bar2")
          ));
      this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
    }

    [Test]
    public void NotFilterCombines()
    {
      var s = new SearchDescriptor<ElasticSearchProject>()
        .From(0)
        .Take(10)
        .Filter(ff =>
          ff.Not(of =>
            of.Term(f => f.Name, "foo")
            && of.Term(f => f.Name, "bar")
          ));
      this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
    }
   
	} 
}
