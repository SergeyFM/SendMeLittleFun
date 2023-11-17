# SendMeLittleFun

The idea behind SendMeLittleFun is pretty straightforward. You add your email address to the list, the app creates a HangFire job and periodically sends a random joke to that address.
I made this app to check what HangFire can do. It's all in Russian so  the following explanations are not translated. ... but you get the idea :)

Главная страница сервиса. Пользователь может добавить свой адрес в рассылку, или удалить адрес из рассылки.
![](README.img/1.png)

Я ввожу почтовый адрес, своё имя, и график рассылки анекдотов.
![](README.img/2.png)

После нажатия кнопки Сохранить, я должен увидеть это подтверждение.
![](README.img/3.png)

Теперь мы можем перейти на страницу планировщика, ссылка HangFire. В повторяющихся задачах можно найти свой адрес. Адрес немного скрыт, чтобы его не спарсили роботы.
![](README.img/4.png)

Общее окно статистики, показывает загрузку планировщика HangFire.
![](README.img/5.png)

А вот и анекдот в моей почте!
![](README.img/6.png)

Теперь я хочу убрать свой адрес из рассылки. Я ввожу его полностью и нажимаю Удалить.
![](README.img/7.png)

Должно выйти подтверждение, что адрес удалён.
![](README.img/8.png)

Это был единственный адрес, поэтому список задач теперь пуст.
![](README.img/9.png)

Желаю вам отличного настроения!