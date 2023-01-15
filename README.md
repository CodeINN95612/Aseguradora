# Aseguradora
App para aseguradora en materia de Metodología de Software
  
## Instrucciones Git.
Este repositorio no funcionará con el sistente de Visual Studio 2022. Seguir las instrucciones.  

### Clonar
1. En una consola, ejecutar el comando:
```bash
git clone https://github.com/CodeINN95612/Aseguradora
```
(Este generara una carpeta llamada Aseguradora en el directorio que se encuentre).

##Subir Cambios.
1. Utilizar el siguiente comando para ver que archivos no se han incluido para ser subidos (debe ser dentro de la carpeta Aseguradora):
```bash
git status
```
2. Si el comando anterior mostro archivos o carpetas en rojo, ejecutar el comando siguiente, caso contrario seguri con el paso 3.
```bash
git add .
```
3. Ejecutar el siguiente comando para crear un commit con una descripcion.
```bash
git commit -m "<descripcion>"
```
4. Ejecutar el siguiente comando para subir los cambios (es posible que pida credenciales de github):
```bash
git push -u origin main
```

## Ejecutar

### Web
1. Abrir en VSCode. (Instalar VSCode y Angular)
2. Instalar los paquetes de Angular, Typescript, HTML.
3. En el archivo `src/app/login/login.component.ts` cambiar, en la linea `29` el URL por el URL de la Web API.
4. En la consola, ejecutar los comandos: 
```bash
npm i
ng serve
```

### API
1. Abrir la solucion en Visual Studio 2022.
2. En el archivo `Aseguradra.Authentication/appSettings.json` cambiar el servidor de la Base de Datos a la correspondiente.
3. Ejecutar el proyecto.
