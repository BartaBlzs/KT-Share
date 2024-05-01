SELECT `autosok`.`nev`, `autosok`.`telefon`, SUM(`javitasok`.`iranyar`) AS 'összeg'
FROM `autosok` INNER JOIN `javitasok` ON `javitasok`.`autos_id` = `autosok`.`autos_id` 
WHERE `autosok`.`telefon` LIKE '(20)%'
GROUP BY `autosok`.`telefon`
ORDER BY `autosok`.`nev`;