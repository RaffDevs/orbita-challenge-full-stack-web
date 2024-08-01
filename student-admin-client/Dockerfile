# Etapa 1: Build
FROM node:18 AS build

# Defina o diretório de trabalho
WORKDIR /app

# Copie os arquivos package.json e package-lock.json para o diretório de trabalho
COPY package*.json ./

# Instale as dependências do projeto
RUN npm install

# Copie todos os arquivos do projeto para o diretório de trabalho
COPY . .

# Execute o comando de build para gerar os arquivos estáticos
RUN npm run build

# Etapa 2: Serve
FROM nginx:alpine

# Copie os arquivos gerados na etapa de build para o diretório do nginx
COPY --from=build /app/dist /usr/share/nginx/html

# Exponha a porta 80 para o servidor web
EXPOSE 80

# Comando padrão para iniciar o servidor nginx
CMD ["nginx", "-g", "daemon off;"]

