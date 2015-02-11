using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Collections;
using Inisoft.Core.Provider;
using Inisoft.Core.Object;
using Inisoft.Core.Interface.Storage;

namespace Inisoft.Core.Interface
{
    /// <summary>
    /// Bazowy interface zapewniający obsługę komunikacji z medium zapisu,
    /// np. Baza danych, plik, API, cokolwiek innego
    /// Każdy provider obiektu biznesowego związany jest z określonym Storage Providerem
    /// </summary>
    public interface IStorageProvider
    {
        /// <summary>
        /// Wykonuje test połączenia
        /// </summary>
        /// <returns></returns>
        MethodResult TestConnection();

        /// <summary>
        /// Sprawdza czy istnieje miejsce w ktorym będą zapamiętywane dane dla obiektu objectName.
        /// Czy jest w bazie danych tabela dla objectName (o tej nazwie)
        /// </summary>
        /// <param name="objectName"></param>
        /// <returns></returns>
        MethodResult CheckObjectStorageExists(ObjectName objectName);

        /// <summary>
        /// Na podstawie definicji obiektu tworzy dla niego tabelę w bazie danych
        /// </summary>
        /// <param name="objectDefinition"></param>
        /// <returns></returns>
        MethodResult CreateStorage(ObjectDefinition objectDefinition);

        /// <summary>
        /// Na podstawie definicji obiektu aktualizuje tablę w bazie danych
        /// </summary>
        /// <param name="objectDefinition"></param>
        /// <returns></returns>
        MethodResult UpdateStorage(ObjectDefinition objectDefinition);

        /// <summary>
        /// Usuwa tabelę z bazy danych dla danej definicji obiektu
        /// </summary>
        /// <param name="objectDefinition"></param>
        /// <returns></returns>
        MethodResult RemoveStorage(ObjectDefinition objectDefinition);

        /// <summary>
        /// Dla danej nazwy obiektu (nazwy tabeli) zwraca opis biznesowy ObjectDefinition
        /// </summary>
        /// <param name="objectName"></param>
        /// <returns></returns>
        MethodResult<ObjectDefinition> GetObjectDefinition(string objectName);
        
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="objectDefinition"></param>
        /// <returns></returns>
        IStorageQueryable<TObject> Select<TObject>(ObjectDefinition objectDefinition)
            where TObject : GenericObject, new();

        MethodResult<TObject> Save<TObject>(TObject genericObject, ObjectDefinition objectDefinition)
                        where TObject : GenericObject;
    }

    public interface IStorageQueryResult<TObject> : IEnumerable<TObject>, IEnumerable
    {
        TObject FirstOrDefault();
        TObject FirstOrDefault(Expression<Func<TObject, bool>> predicate);
    }

    public interface IStorageQueryable<TObject> : IStorageQueryResult<TObject>
    {
        IStorageQueryable<TObject> Where(Expression<Func<TObject, bool>> predicate);
        IStorageQueryable<TObject> OrderBy(Expression<Func<TObject, bool>> predicate);

        IStorageQueryResult<TObject> ByQuery(IStorageQuery query);

        string GetQuery();
    }
}