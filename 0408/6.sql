SELECT `egyesulet`.`enev` AS 'egyesület neve',
COUNT(*) AS 'versenyzők száma'
FROM futo
INNER JOIN egyesulet ON `egyesulet`.`eazon` = `futo`.`eazon`
GROUP BY `futo`.`eazon`;