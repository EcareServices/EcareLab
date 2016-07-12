# (Azure) Sql Full Text Search

## The project

Open and inspect the project. Make sure the project builds properly.

Go to the web.config and add a valid connectionstring for `WikiConnection` with a catalog named `WikiReferences`. Make sure the Azure SQL server has at least V12 and the connection string has sufficient rights to create a database.

Go to the `Package Manager Console` and run the command `update-database` to create and seed the database.

The database has one table `WikiReferences` with wikipedia url, title and text content. 

## Set up full text search

Open SSMS and connect with the server. Select `New Query` on the WikiReferences database.

First we need are going to create a full text catalog. Execute the following command:

```sql
CREATE FULLTEXT CATALOG ftCatalog AS DEFAULT;
```

The catalog should be visible in SSMS explorer in 'WikiReferences/Storage/Full Text Catalogs'.

Now we are going to add a Full Text Index on the `Content` column of the `WikiReferences` table. In the query window execute the following command:

```sql
CREATE FULLTEXT INDEX ON WikiReferences(Content Language 1043) KEY INDEX [PK_dbo.WikiReferences] ON ftCatalog;
```

Language 1043 is the LCID code for the Dutch language

Now we need to enable the index.

```sql
ALTER FULLTEXT INDEX ON WikiReferences ENABLE; 
GO 
ALTER FULLTEXT INDEX ON WikiReferences START FULL POPULATION;
```

## Full Text search with SQL

Now we are able to query the column with these new syntaxes

```sql
SELECT Id, Title
FROM WikiReferences
WHERE CONTAINS(Content, 'onmiddellijk OR terugkeer OR heerser');
```

```sql
SELECT Id, Title
FROM WikiReferences
WHERE CONTAINS (Content, '"rijksm*"' ) 
```

But what you properbly want is sort on the ranking. This kan be done using a join on `CONTAINSTABLE`.

```sql
SELECT r.Id, t.Title, KEY_TBL.RANK   
FROM WikiReferences r 
	JOIN CONTAINSTABLE(WikiReferences, Content, 'onmiddellijk OR terugkeer OR heerser' ) AS KEY_TBL  
		ON r.Id = KEY_TBL.[KEY]  
ORDER BY KEY_TBL.RANK DESC 
```

Check out more on :[Query with Full-Text Search on MSDN](https://msdn.microsoft.com/en-us/library/ms142583.aspx)

You can also check the status of the index using this command:

```sql
SELECT * FROM sys.dm_fts_index_population
```

You can also see what is inside a index:

```sql
SELECT * FROM sys.dm_fts_index_keywords( DB_ID('WikiReferences'), OBJECT_ID('WikiReferences'))
```

## Simple example Full Text search with Entity Framework

With Entity framework version 6 you can use query interceptors to rewrite queries.

In the project check out `/Db/FtsInterceptor.cs` and the `RewriteFullTextQuery` method. Sql is written for this function.

Open the file `HomeController.cs` and goto the post method to see the usage of the interceptor.

Run the project. You should be able to search the content with different key words.

In EF 7 it will be easier using  FromSql, check [Example](https://github.com/rowanmiller/UnicornStore/blob/master/UnicornStore/src/UnicornStore/Controllers/ShopController.cs#L92)