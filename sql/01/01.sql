SELECT DISTINCT(i.value * j.value)
FROM input i
CROSS JOIN input j 
WHERE i.value + j.value = 2020