# Neumaticos Del Cibao
## Workflow:
### Branches de Funcionamiento basico
#### Master
Branch principal de desarrollo. En este se mantendra el ultimo estado funcional y probado del software.

#### QA
Branch utilizado para prueba. Al terminar una tarea, se subira a este branch con el objetivo de que todos los demas desarrolladores y el cliente final puedan probar el funcionamiento del feature agregado.

#### Dev
Branch utilizado entre developers con el objetivo de BETA testing, criticas y revisiones de funcionamiento general. Basicamente, es un branch que pretende ser usado para ayudas entre uno y otro desarrollador.

### Agregando Features
Al agregar/crear un feature se sacara un branch de la parte de produccion o master.  Utilizar el comando  

```shell
git checkout master && git checkout -b [feature-name]
```
