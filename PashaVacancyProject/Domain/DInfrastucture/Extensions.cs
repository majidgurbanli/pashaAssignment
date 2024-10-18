using Microsoft.EntityFrameworkCore;
using PashaVacancyProject.Logic.Infrastucture.Paiging;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

namespace PashaVacancyProject.Domain.DInfrastucture
{
    public static class Extensions
    {

        public static void DetachAllEntities(this BoxDbContent Source)
        {

            var Entries = Source.ChangeTracker.Entries().ToList();

            var changedEntriesCopy = Entries.Where(e => e.State == EntityState.Added || e.State == EntityState.Modified ||
                                                     e.State == EntityState.Deleted || e.State == EntityState.Unchanged)
                .ToList();

            foreach (var entry in changedEntriesCopy)
                entry.State = EntityState.Detached;
        }
        public static List<string> UpdatedProperties<T>(this Expression<Func<T, object>>[] Source, DbContext Context)
        {

            var AllUpdatedProperties = new List<string>();

            Source.ToList().ForEach(x =>
            {
                var MemberExpression = x.Body as MemberExpression;
                var UnaryExpression = x.Body as UnaryExpression;

                string PropertyName = MemberExpression != null ? MemberExpression.Member.Name : ((MemberExpression)UnaryExpression.Operand).Member.Name;

                AllUpdatedProperties.Add(PropertyName);
            });


            return AllUpdatedProperties;
        }
        public static IEnumerable<List<T>> IEnumerablePartition<T>(this List<T> source, Int32 size)
        {
            for (int i = 0; i < Math.Ceiling(source.Count / (Double)size); i++)
                yield return new List<T>(source.Skip(size * i).Take(size)).ToList();
        }
        public static IEnumerable<List<T>> IEnumerablePartition<T>(this IEnumerable<T> source, Int32 size)
        {
            for (int i = 0; i < Math.Ceiling(source.Count() / (Double)size); i++)
                yield return new List<T>(source.Skip(size * i).Take(size)).ToList();
        }

        public static string GetDisplayName(Enum value)
        {
            var displayAttribute = (DisplayAttribute)value.GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(DisplayAttribute), false)[0];
            return displayAttribute.Name;
        }
        public static IQueryable<TSource> OrderByProperty<TSource>(this IQueryable<TSource> _Source, string Order, SortOrderCust _Sort)
        {


            ParameterExpression _ParameterExpression = Expression.Parameter(typeof(TSource), "posting");



            var AllProperties = _ParameterExpression.Type.GetProperties().ToList();

            var PagingOrderPropertyName = AllProperties.Where(x => x.Name == Order).FirstOrDefault();


            var DefaultOrderPropertyName = AllProperties.Where(x => x.Name.IndexOf("ID") >= 0).FirstOrDefault();

            var OrderPropertyName = (PagingOrderPropertyName == null ? DefaultOrderPropertyName : PagingOrderPropertyName) ?? AllProperties.FirstOrDefault();



            Expression _Expression = Expression.Property(_ParameterExpression, OrderPropertyName.Name);

            LambdaExpression _LambdaExpression = Expression.Lambda(_Expression, new[] { _ParameterExpression });


            MethodInfo _OrderByMethod = typeof(Queryable).GetMethods()
                                                         .Where(method => method.Name == (_Sort == SortOrderCust.Desc ? "OrderByDescending" : "OrderBy"))
                                                         .Where(method => method.GetParameters().Length == 2)
                                                         .Single();

            MethodInfo _GenericMethod = _OrderByMethod.MakeGenericMethod(new[] { typeof(TSource), _Expression.Type });

            return (IQueryable<TSource>)_GenericMethod.Invoke(null, new object[] { _Source, _LambdaExpression });

        }


        public static Expression<Func<T, bool>> CombineExpression<T>(this Expression<Func<T, bool>> BaseExpession, List<Expression<Func<T, bool>>> Expressions)
        {
            if (Expressions != null)
            {
                Expressions.ForEach(Expression =>
                {
                    if (Expression != null)
                    {
                        BaseExpession = BaseExpession.And(Expression);
                    }
                });
            }

            return BaseExpession;
        }
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> FirstExpression, Expression<Func<T, bool>> SecondExpression)
        {
            return FirstExpression.Compose(SecondExpression, Expression.And);
        }
        public static Expression<T> Compose<T>(this Expression<T> FirstExpression, Expression<T> SecondExpression, Func<Expression, Expression, Expression> Merge)
        {
            var ParametrMap = FirstExpression.Parameters.Select((f, i) => new { f, s = SecondExpression.Parameters[i] }).ToDictionary(p => p.s, p => p.f);

            var SecondBody = ParameterRebinder.ReplaceParameters(ParametrMap, SecondExpression.Body);

            return Expression.Lambda<T>(Merge(FirstExpression.Body, SecondBody), FirstExpression.Parameters); ;
        }
        public class ParameterRebinder : ExpressionVisitor
        {

            private readonly Dictionary<ParameterExpression, ParameterExpression> map;

            public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
            {
                this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
            }

            public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
            {
                return new ParameterRebinder(map).Visit(exp);
            }

            protected override Expression VisitParameter(ParameterExpression p)
            {
                ParameterExpression replacement;


                if (map.TryGetValue(p, out replacement))
                {
                    p = replacement;
                }

                return base.VisitParameter(p);
            }
        }
    }
}
