# QuestionApps
 POST /api/Question/CreateQuestion
Implementation:
Accepts QuestionDtos.
Maps to Question entity with AutoMapper.
Saves entity via QuestionService.AddQuestion.
Returns created question's ID and data.

Endpoint: PUT /api/Question/EditQuestion/{id}
Implementation:
Accepts question ID and QuestionDtos.
Fetches existing question with QuestionService.GetQuestionById.
Updates properties and saves changes with QuestionService.UpdateQuestion.

Endpoint: GET /api/Question/GetAllQuestions
Implementation:
Fetches all questions with QuestionService.GetAllQuestions.
Maps to QuestionDtos and returns list.

Endpoint: POST /api/Question/SubmitResponses
Implementation:
Accepts ResponseDtos list.
Validates responses.
Checks if question IDs exist.
Maps to Response entities and saves with QuestionService.AddResponses.
Returns saved responses as ResponseDtos.

Online Database: Hosted on Render.
Connection String: Configured in appsettings.json for remote PostgreSQL database access.
