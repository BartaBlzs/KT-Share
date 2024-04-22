SELECT e.enev AS enev, f.fnev AS fnev
FROM futo f JOIN egyesulet e ON f.eazon = e.eazon
ORDER BY 1, 2;