# Getting Started
## How to Build

The generated code uses a few NuGet Packages e.g., Newtonsoft.Json, UniRest,
and Microsoft Base Class Library. The reference to these packages is already
added as in the packages.config file. If the automatic NuGet package restore
is enabled, these dependencies will be installed automatically. Therefore,
you will need internet access for build.
     
1. Open the solution (RewardSciences.sln) file.
2. Invoke the build process using `Ctrl+Shift+B` shortcut key or using the `Build` menu as shown below.

![Building SDK using Visual Studio](http://apidocs.io/illustration/cs?step=buildSDK&workspaceFolder=Reward Sciences-CSharp&workspaceName=RewardSciences&projectName=RewardSciences.PCL)

## How to Use

The build process generates a portable class library, which can be used like a normal class library. The generated library is compatible with Windows Forms, Windows RT, Windows Phone 8,
 Silverlight 5, Xamarin iOS, Xamarin Android and Mono. More information on how to use can be found at the following link. 
http://msdn.microsoft.com/en-us/library/vstudio/gg597391(v=vs.100).aspx

The following section explains how to use the RewardSciences library in a new console project.     
    
#### 1. Starting a new project
For starting a new project, right click on the current solution from the *solution explorer* and choose  ``` Add -> New Project ```.

![Add a new project in the existing solution using Visual Studio](http://apidocs.io/illustration/cs?step=addProject&workspaceFolder=Reward Sciences-CSharp&workspaceName=RewardSciences&projectName=RewardSciences.PCL)

Next, choose "Console Application", provide a ``` TestConsoleProject ``` as the project name and click ``` OK ```.

![Create a new console project using Visual Studio](http://apidocs.io/illustration/cs?step=createProject&workspaceFolder=Reward Sciences-CSharp&workspaceName=RewardSciences&projectName=RewardSciences.PCL)


#### 2. Set as startup project
The new console project is the entry point for the eventual execution. This requires us to set the ``` TestConsoleProject ``` as the start-up project. To do this, right-click on the  ``` TestConsoleProject ``` and choose  ``` Set as StartUp Project ``` form the context menu.

![Set the new cosole project as the start up project](http://apidocs.io/illustration/cs?step=setStartup&workspaceFolder=Reward Sciences-CSharp&workspaceName=RewardSciences&projectName=RewardSciences.PCL)


#### 3. Add reference of the library project
In order to use the RewardSciences library in the new project, first we must add a projet reference to the ``` TestConsoleProject ```. First, right click on the ``` References ``` node in the *solution explorer* and click ``` Add Reference... ```.

![Open references of the TestConsoleProject](http://apidocs.io/illustration/cs?step=addReference&workspaceFolder=Reward Sciences-CSharp&workspaceName=RewardSciences&projectName=RewardSciences.PCL)

Next, a window will be displayed where we must set the ``` checkbox ``` on ``` RewardSciences.PCL ``` and click ``` OK ```. By doing this, we have added a reference of the ```RewardSciences.PCL``` project into the new ``` TestConsoleProject ```.

![Add a reference to the TestConsoleProject](http://apidocs.io/illustration/cs?step=createReference&workspaceFolder=Reward Sciences-CSharp&workspaceName=RewardSciences&projectName=RewardSciences.PCL)


#### 4. Write sample code
Once the ``` TestConsoleProject ``` is created, a file named ``` Program.cs ``` will be visible in the *solution explorer* with an empty ``` Main ``` method. This is the entry point for the execution of the entire solution.
Here, you can add code to initialize the client library and acquire the instance of a *Controller* class. Sample code to initialize the client library and using controller methods is given in the subsequent sections.

![Add a reference to the TestConsoleProject](http://apidocs.io/illustration/cs?step=addCode&workspaceFolder=Reward Sciences-CSharp&workspaceName=RewardSciences&projectName=RewardSciences.PCL)

## How to Test

The generated SDK also contain one or more Tests, which are contained in the Tests project.
In order to invoke these test cases, you will need *NUnit 3.0 Test Adapter Extension for Visual Studio*.
Once the SDK is complied, the test cases should appear in the Test Explorer window.
Here, you can click *Run All* to execute these test cases.

## Initialization

#### Authentication and Initialization
In order to setup authentication and initialization of the API client, you need the following information.

| Parameter | Description |
|-----------|-------------|
| oAuthAccessToken | The OAuth 2.0 access token to be set before API calls |



API client can be initialized as following.

```csharp
// Configuration parameters and credentials
string oAuthAccessToken = "oAuthAccessToken"; // The OAuth 2.0 access token to be set before API calls

RewardSciencesClient client = new RewardSciencesClient(oAuthAccessToken);
```

# Class Reference
## <a name="list_of_controllers"></a>List of Controllers

* [Rewards](#rewards)
* [RewardCategories](#reward_categories)
* [Users](#users)
* [Activities](#activities)

## <a name="rewards"></a>![Class: ](http://apidocs.io/img/class.png "RewardSciences.PCL.Controllers.Rewards") Rewards

#### Get singleton instance
The singleton instance of the ``` Rewards ``` class can be accessed from the API Client.
```csharp
IRewards rewards = client.Rewards;
```

### <a name="bid"></a>![Method: ](http://apidocs.io/img/method.png "RewardSciences.PCL.Controllers.Rewards.Bid") Bid

> Bid on a reward auction.

```csharp
Task<dynamic> Bid(int userId, int rewardId, string amount)
```

#### Parameters: 

| Parameter | Tags | Description |
|-----------|------|-------------|
| userId |  ``` Required ```  | The ID of the user who is bidding on the reward auction. |
| rewardId |  ``` Required ```  | The ID of the reward auction to be bid on. |
| amount |  ``` Required ```  | Can be either 'max' (when max bidding) or the number of points the user wants to bid. |



#### Example Usage:
```csharp
int userId = 240;
int rewardId = 240;
string amount = "amount";

dynamic result = await rewards.Bid(userId, rewardId, amount);

```





### <a name="list"></a>![Method: ](http://apidocs.io/img/method.png "RewardSciences.PCL.Controllers.Rewards.List") List

> List all the available rewards.

```csharp
Task<dynamic> List(int? categoryId = null, int? limit = 25, int? offset = 0)
```

#### Parameters: 

| Parameter | Tags | Description |
|-----------|------|-------------|
| categoryId |  ``` Optional ```  | The id of the category to filter rewards by |
| limit |  ``` Optional ```  ``` DefaultValue ```  | The number of rewards you want to be retrieved. |
| offset |  ``` Optional ```  ``` DefaultValue ```  | The number of rewards you want to skip before starting the retrieval. |



#### Example Usage:
```csharp
int? categoryId = 240;
int? limit = 25;
int? offset = 0;

dynamic result = await rewards.List(categoryId, limit, offset);

```





### <a name="redeem"></a>![Method: ](http://apidocs.io/img/method.png "RewardSciences.PCL.Controllers.Rewards.Redeem") Redeem

> Redeem a reward.

```csharp
Task<dynamic> Redeem(int userId, int rewardId)
```

#### Parameters: 

| Parameter | Tags | Description |
|-----------|------|-------------|
| userId |  ``` Required ```  | The ID of the user who is redeeming the reward. |
| rewardId |  ``` Required ```  | The ID of the reward to be redeemed. |



#### Example Usage:
```csharp
int userId = 198;
int rewardId = 198;

dynamic result = await rewards.Redeem(userId, rewardId);

```





### <a name="show"></a>![Method: ](http://apidocs.io/img/method.png "RewardSciences.PCL.Controllers.Rewards.Show") Show

> Show a reward's details.

```csharp
Task<dynamic> Show(int rewardId)
```

#### Parameters: 

| Parameter | Tags | Description |
|-----------|------|-------------|
| rewardId |  ``` Required ```  | The ID of the reward to be retrieved. |



#### Example Usage:
```csharp
int rewardId = 198;

dynamic result = await rewards.Show(rewardId);

```





[Back to List of Controllers](#list_of_controllers)
## <a name="reward_categories"></a>![Class: ](http://apidocs.io/img/class.png "RewardSciences.PCL.Controllers.RewardCategories") RewardCategories

#### Get singleton instance
The singleton instance of the ``` RewardCategories ``` class can be accessed from the API Client.
```csharp
IRewardCategories rewardCategories = client.RewardCategories;
```

### <a name="list"></a>![Method: ](http://apidocs.io/img/method.png "RewardSciences.PCL.Controllers.RewardCategories.List") List

> List all the available reward categories.

```csharp
Task<dynamic> List(int? limit = 25, int? offset = 0)
```

#### Parameters: 

| Parameter | Tags | Description |
|-----------|------|-------------|
| limit |  ``` Optional ```  ``` DefaultValue ```  | The number of reward categories you want to be retrieved. |
| offset |  ``` Optional ```  ``` DefaultValue ```  | The number of reward categories you want to skip before starting the retrieval. |



#### Example Usage:
```csharp
int? limit = 25;
int? offset = 0;

dynamic result = await rewardCategories.List(limit, offset);

```





[Back to List of Controllers](#list_of_controllers)
## <a name="users"></a>![Class: ](http://apidocs.io/img/class.png "RewardSciences.PCL.Controllers.Users") Users

#### Get singleton instance
The singleton instance of the ``` Users ``` class can be accessed from the API Client.
```csharp
IUsers users = client.Users;
```

### <a name="show"></a>![Method: ](http://apidocs.io/img/method.png "RewardSciences.PCL.Controllers.Users.Show") Show

> This endpoint lets retrieve a user's details.

```csharp
Task<dynamic> Show(int userId)
```

#### Parameters: 

| Parameter | Tags | Description |
|-----------|------|-------------|
| userId |  ``` Required ```  | The ID of the user to be retrieved. |



#### Example Usage:
```csharp
int userId = 198;

dynamic result = await users.Show(userId);

```





### <a name="identify"></a>![Method: ](http://apidocs.io/img/method.png "RewardSciences.PCL.Controllers.Users.Identify") Identify

> This endpoint lets you tie a user with his/her activities. You’ll want to identify a user with any relevant information as soon as they log-in or sign-up.

```csharp
Task<dynamic> Identify(string email, string firstName = null, string lastName = null)
```

#### Parameters: 

| Parameter | Tags | Description |
|-----------|------|-------------|
| email |  ``` Required ```  | The user's email address |
| firstName |  ``` Optional ```  | The user's first name |
| lastName |  ``` Optional ```  | The user's last name |



#### Example Usage:
```csharp
string email = "email";
string firstName = "first_name";
string lastName = "last_name";

dynamic result = await users.Identify(email, firstName, lastName);

```





[Back to List of Controllers](#list_of_controllers)
## <a name="activities"></a>![Class: ](http://apidocs.io/img/class.png "RewardSciences.PCL.Controllers.Activities") Activities

#### Get singleton instance
The singleton instance of the ``` Activities ``` class can be accessed from the API Client.
```csharp
IActivities activities = client.Activities;
```

### <a name="track"></a>![Method: ](http://apidocs.io/img/method.png "RewardSciences.PCL.Controllers.Activities.Track") Track

> This endpoint lets you track the activities your users perform.

```csharp
Task<dynamic> Track(
        int userId,
        string activityType,
        int? price = null,
        string recordId = null)
```

#### Parameters: 

| Parameter | Tags | Description |
|-----------|------|-------------|
| userId |  ``` Required ```  | The ID of the user who is performing the activity. |
| activityType |  ``` Required ```  | The type of activity the user is performing. Example: 'purchased-a-product' |
| price |  ``` Optional ```  | The price related to the activity, if any. Expressed in USD |
| recordId |  ``` Optional ```  | The ID for the record associated with the activity in your database. |



#### Example Usage:
```csharp
int userId = 198;
string activityType = "activity_type";
int? price = 198;
string recordId = "record_id";

dynamic result = await activities.Track(userId, activityType, price, recordId);

```





[Back to List of Controllers](#list_of_controllers)


