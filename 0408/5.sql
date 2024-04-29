SELECT `verseny`.`vnev` AS 'verseny neve',
COUNT(*) AS 'indulók száma' 
FROM eredmeny 
INNER JOIN verseny ON `verseny`.`vazon` = `eredmeny`.`vazon`
GROUP BY `eredmeny`.`vazon`;