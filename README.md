# BlogCodeReview

This project has two main entities to exemplify the code review: Post and Tag.

The main objective is to create classes that, following the DDD, can map Tags not only to Post entities but to any entity that may appear in the future, for example a Product entity, or an Image entity.

The basic functionality of this example is a post CRUD.

A post contains one or more tags. During CRUD operations a list of Tags is assigned to the Post entity.

The UI editor for the post tag list is a textbox. The user types the tag names separated by comas.

How can this code be improved in order to follow functional principles and DDD?
