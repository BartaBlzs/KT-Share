SELECT `futo`.`fnev` AS 'név',
`egyesulet`.`eazon` AS 'egyesület azonosító',
`eredmeny`.`ido` AS 'idő'
 FROM verseny
INNER JOIN eredmeny ON `eredmeny`.`vazon` = `verseny`.`vazon`
INNER JOIN futo ON `futo`.`fazon` = `eredmeny`.`fazon`
INNER JOIN egyesulet ON `egyesulet`.`eazon` = `futo`.`eazon`
WHERE`verseny`.`vnev` = 'Olimpia'
ORDER BY `eredmeny`.`ido`;