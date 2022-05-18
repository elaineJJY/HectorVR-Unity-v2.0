# Hector VR v2.0

## Introduction

Ground-based rescue robot simulation with 4 operating modes and test scenarios



## Setup

**In theory, you can run the project directly by downloading and setting up SteamVR Input.**



##### 0. Install

For similar pop-up alerts please click:

![image-20210628133300487](https://raw.githubusercontent.com/elaineJJY/Storage/main/Picture/20210628133300.png?token=APASYCSPQNQGUAURUL3L3I3A3GZ22)



##### 1. SteamVR Input

Set SteamVR bindings under `Window>SteamVR Input>Open binding UI`



##### 2. Navmesh Agent

The project uses the NavMesh to allow it to navigate the Scene. Please make sure there is a corresponding Navmesh generated folder in the Scene folder. If the navigation does not work after running, please delete the origin folder and re-bake the scene under `Window>AI>Navigation`.

![image-20210719140935444](https://raw.githubusercontent.com/elaineJJY/Storage/main/Picture/20210719140942.png)



#### 3. Set the path to save the .csv file

see TestManagement below



## Operation Mode

##### 1. Handle 

The user can control the robot directly by operating the handle

<img src="https://raw.githubusercontent.com/elaineJJY/Storage/main/Picture/20210719141931.png" alt="handle1" style="zoom: 50%;" />

![handle2](https://raw.githubusercontent.com/elaineJJY/Storage/main/Picture/20210719145003.png)

##### 2. Lab

The environment is designed to resemble a real laboratory environment as closely as possible, and is operated by holding the joystick or clicking on buttons with the virtual hand.

![image-20210628133617313](https://raw.githubusercontent.com/elaineJJY/Storage/main/Picture/20210628133617.png?token=APASYCURCHKGAIPQKLLAMDDA3G2HE)

The part that involves virtual joystick movement and button effects uses an open source github project [VRtwix](https://github.com/rav3dev/vrtwix).

##### 3. Remote Mode

In this mode the user can pick up the tools of operation manually: for example the remote control. Alternatively, the target point can be set directly from the right hand. The robot will automatically run to the set target point.

![remote3](https://raw.githubusercontent.com/elaineJJY/Storage/main/Picture/20210719142041.png)

##### 4. UI Mode

In this mode the user must use the rays emitted by the handle to manipulate the robot. By clicking on the direction button, the user can control the direction of movement of the robot. In addition to this, the user can also turn on the follow function, in which case the robot will always follow the user's position in the virtual world.

![Snipaste_2021-07-17_18-53-26](https://raw.githubusercontent.com/elaineJJY/Storage/main/Picture/20210719142014.png)





## Test Scene

In the test scenario, the user is asked to find as many sufferers as possible within a limited period of time. The test is terminated when the time runs out or when all victims have been found.
Throughout the test, the robot's performance, including the total distance travelled, the average speed travelled and the number of collisions, is recorded. In addition to this, the rescue of victims, for example if they are already in view but ignored by the user, is also recorded.

![image-20210628134138882](https://raw.githubusercontent.com/elaineJJY/Storage/main/Picture/20210628155701.png)

###### Collision Detection

Each object in the scene that will be used for collision detection comes with a suitable collision body.

![image-20210628134930493](https://raw.githubusercontent.com/elaineJJY/Storage/main/Picture/20210628134930.png?token=APASYCU266JS7J3J7GACYUDA3G3YY)



###### Scene overview

![image-20210628133946878](https://raw.githubusercontent.com/elaineJJY/Storage/main/Picture/20210628133946.png?token=APASYCSJGIXOJI7FT7DPDB3A3G2UI)

![image-20210628134229529](https://raw.githubusercontent.com/elaineJJY/Storage/main/Picture/20210628134229.png?token=APASYCUJPRZNCGUHHI62CGDA3G26M)

![image-20210628134255927](https://raw.githubusercontent.com/elaineJJY/Storage/main/Picture/20210628134256.png?token=APASYCRBWBQBEHGCM3CIMZ3A3G3AA)

![image-20210628134325451](https://raw.githubusercontent.com/elaineJJY/Storage/main/Picture/20210628134325.png?token=APASYCVJWFMPXJFHCOCIFT3A3G3B4)





## Robot

To simulate the process of a robot using a LiDAR to detect the real environment and synchronise it to Unity, a sphere collision body was set up on the robot.

![image-20210703194322743](https://raw.githubusercontent.com/elaineJJY/Storage/main/Picture/20210703194329.png)

![image-20210715101634609](https://raw.githubusercontent.com/elaineJJY/Storage/main/Picture/20210715101634.png)

![image-20210715102103393](https://raw.githubusercontent.com/elaineJJY/Storage/main/Picture/20210715102103.png)

![image-20210715101904585](https://raw.githubusercontent.com/elaineJJY/Storage/main/Picture/20210715101904.png)



## TestManagement

The order of the tests depends on *Latin-Square* with size =4. 

#### TestID & .csv file

The **TestID** (sequential number of this participant) must be set before testing, as well as **the path** to store the .csv test data.

The .csv file should be closed during the test.

A possible folder path can be:

```
C:\Users\Desktop\TestResult
```

 After the time is out, or all the victims are rescued, 

1.  The test will be ended
2.  .csv data will be recorded
3.  Clicking the "Ende Test" Button will lead to the next operation mode

![image-20210706102437503](https://raw.githubusercontent.com/elaineJJY/Storage/main/Picture/20210706102444.png)



#### How to stop the test immediately

If you want to stop the current test immediately, change the value of  `Total Time`. When the value of  `Total Time` is 0, the test will automatically end and the test data will be saved.

If you click End Test when the test is not really stopped, you will return to the Simulation scene and remain in the same operation mode.

