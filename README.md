# Сайт по фотографиям AskanioPhotoSite
<hr>

На данный момент реализовано:

Собственная реализация провайдера для хранения данных в текстом файле. Включает в себя обобщенный репозиторий, взаимодействующий с хранилищем, которое обеспечивает чтение и запись данных, сохранение полученных результатов в кэш, извлечение из кэша, обновление кэша, добавление запросов на выполнение в пул транзакций, создание бэкапов, выполнение и откат транзакций.
Взаимодействие репозитория и хранилища  построено на основании составления и последующего чтения запроса на выполнения. За преобразование данных в текстовый формат и  обратно в сущности отвечает обобщенный интерпретатор.
