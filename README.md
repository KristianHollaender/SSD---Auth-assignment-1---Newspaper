# Secure Software Development - Authorization Exercise
Imagine you are working on the back-end for a news site. The site contains news articles written by journalists. The site have editors which can correct factual mistakes in articles and remove hateful comments.

The business model rely on subscription fees and advertising. Subscribers can, for a small monthly fee access a version of the site without adds. Guests can still read articles published on the site, but will be presented with ads.

## ASP.NET Core Identity is used to solve Authorization for this Exercise
![1689515031126](https://github.com/user-attachments/assets/54992f2d-f981-4e0d-a493-98d36e5517e2)



### How to compose application via Docker: 
1. Go to `/BasicAuthApi/` (root of solution) and do following command
```
docker compose up --build
```
2. Go to `localhost:3000/` or use an API Platform (such as Postman)

#### Here is a list of commands to try out:

> Non-authorized
```
GET /Articles/GetAllArticles
GET /Comments/GetCommentsOnArticle
``` 
>
> Authorized
>
```
POST /Articles/CreateArticle
GET /Articles/GetArticle/{articleId}
PUT /Articles/EditArticle/{articleId} 
DElETE /Articles/DeleteArticle/{articleId}

POST /Comments/CreateComment
GET /Comments/GetComment/{commentId}
PUT /Comments/EditComment/{commentId}
DELETE /Comments/DeleteComment/{commentId}
```

### OBS: To use authorization, do a login and use bearer token
### Here is a list regarding all Asp .NET Core identity features
<img src="https://github.com/user-attachments/assets/923d1c93-9e48-4d38-be05-718c3231c1d2" width="500"/>


## USAGE 

#### GetAllArticles (No Auth needed - Guest)
<img src="https://github.com/user-attachments/assets/2ac4fb76-8c8e-4000-8c80-a4e0ddf02377" width="300"/>


#### CreateArticle (Auth needed - Writer+)
> This is without Authorization token filled in
<img src="https://github.com/user-attachments/assets/6b91cd95-8e9e-4e20-9abe-f645fafd2a0d" height="500"/>

<br>
<br>
<br>
<br>

> This is with the Auth token
<img src="https://github.com/user-attachments/assets/77ad0c8f-c4e3-4af9-9558-12334c7c9348" height="600"/>



