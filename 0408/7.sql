SELECT vnev AS 'verseny neve',
helyszin AS 'helyszín',
idopont AS 'verseny időpontja' 
FROM verseny 
WHERE YEAR(idopont) = 2001
ORDER BY idopont
LIMIT 1;