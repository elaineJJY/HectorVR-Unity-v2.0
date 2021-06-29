# Abstract



# Introduction

1.5 Seite

> ##### Rescue Robot
>
> - What is rescue robots & some use cases of rescue robots
> - Existed control methods of robots

In recent years, natural disasters such as earthquakes, tsunamis and potential nuclear, chemical, biological and explosives have seriously threatened the safety of human life and property. While the number of various disasters has increased, their severity, diversity and complexity have also gradually increased. The 72h after a disaster is the golden rescue time, but the unstructured environment of the disaster site makes it difficult for rescuers to work quickly, efficiently and safely.

Rescue robots have the advantages of high mobility and handling breaking capacity, can work continuously to improve the efficiency of search and rescue, and can achieve the detection of graph, sound, gas and temperature within the ruins by carrying a variety of sensors, etc.
Moreover, the robot rescue can assist or replace the rescuers to avoid the injuries caused by the secondary collapse and reduce the risk of rescuers. Therefore, rescue robots have become an important development direction.

In fact, rescue robots have been put to use in a number of disaster scenarios. The Center for Robot-Assisted Search and Rescue (CRASAR) used rescue robots for Urban Search and Rescue (USAR) task during the World Trade Center collapse in 2001 \cite{Casper:2003tk} and has employed rescue robots at multiple disaster sites in the years since to assist in finding survivors, inspecting buildings and scouting the site environment etc \cite{Murphy:2012th}. Anchor Diver III was utilized as underwater support to search for bodies drowned at sea after the 2011 Tohoku Earthquake and Tsunami \cite{Huang:2011wq}.

Considering the training time and space constraints for rescuers \cite{Murphy:2004wl}, and the goal of eﬃciency and ﬂuency collaboration \cite{10.1145/1228716.1228718}, the appropriate human-robot interaction approach deserves to be investigated. Some of the existing human-computer interaction methods are Android software \cite{Sarkar:2017tt} \cite{Faisal:2019uu}, gesture recognition\cite{Sousa:2017tn} \cite{10.1145/2157689.2157818} \cite{Nagi:2014vu}, facial voice recognition \cite{Pourmehr:2013ta}, adopting eye movements \cite{Ma:2015wu}, Augmented Reality(AR)\cite{SOARES20151656} and Virtual Reality(VR), etc.



> ##### VR and robot
>
> ###### What is VR
>
> 
>
> ###### VR Advantage
>
> - general advantages
>- advantage regarding robots
> 
>###### VR limatation and challenges
> 
> - disadvantages
>- challenges:  improve the level of human-computer integration.
> - There remains a need to ...

Among them, VR has gained a lot of attention due to its immersion and the interaction method that can be changed virtually. VR is no longer a new word. With the development of technology in recent years, VR devices are gradually becoming more accessible to users. With the improvement of hardware devices, the new generation of VR headsets have higher resolution and wider field of view. And in terms of handle positioning, with the development of computer vision in the past few years, VR devices can now use only the four cameras mounted on the VR headset to achieve accurate spatial positioning, and support hand tracking, accurately capturing every movement of hand joints. While VR are often considered entertainment devices, VR brings more than that. It plays an important role in many fields such as entertainment, training, education and medical care.

The use of VR in human-computer collaboration also has the potential. In terms of reliability, VR is reliable as a novel alternative to human-robot interaction. The interaction tasks that users can accomplish with VR devices do not differ significantly from those using real operating systems\cite{Villani:2018ub}. In terms of user experience and operational efficiency, VR displays can provide users with stereo viewing cues, which makes collaborative human-robot interaction tasks in certain situations more efficient and performance better \cite{Liu:2017tw}.

However, there remains a need to explore human-computer interaction patterns and improve the level of human-computer integration\cite{Wang:2017uy}. Intuitive and easy-to-use interaction methods can enable the user to explore the environment as intentionally as possible and improve the efficiency of search and rescue.



> ##### What I have done (overview)
>
> ###### Unity Project
>
> - main goal
> - 4 modes
>
> - test scenes
>
> 
>
> ###### User Study
>
> - Testing process
>
> - General content of the survey

For this purpose, this paper presents a preliminary VR-based system for the simulation of ground rescue robots with four different modes of operation and corresponding test scenarios imitating a post-disaster city. The test scenario simulates a robot collaborating with Unity to construct a virtual 3D scene. The robot has a simulated radar, which makes the display of the scene dependent on the robot's movement. In order to find a control method that is as intuitive and low mental fatigue as possible, a user survey was executed after the development was completed.



> ##### Paper Architecture

Section \ref{*i*mplementation} provides details of the purposed system, including the techniques used for the different interaction modes and the structure of the test scenarios.
Section \ref{evaluate} will talk about the design and process of user study.

Section \ref{result} presents the results of the user study and analyzes the advantages and disadvantages of the different modes of operation and the directions for improvement.

Finally, in Section \ref{conclusion}, conclusions and future work are summarized.



# (Related Work)



# Implementation

% summary

In this chapter, the tools and techniques used in building this human-computer collaborative VR-based system are described. The focus will be on interaction techniques for different modes of operation. In addition, the construction of test scenarios and the setup of the robot will also be covered in this chapter.



## 1. Overview

> - the purpose of the unity project
> - Components of the project: 4 operation modes & test Scene

The main goal of this work is to design and implement a VR-based human-robot collaboration system with different methods of operating the robot in order to find out which method of operation is more suitable to be used to control the rescue robot. Further, it is to provide some basic insights for future development directions and to provide a general direction for finding an intuitive, easy-to-use and efficient operation method. Therefore, the proposed system was developed using Unity, including four modes of operation and a corresponding test environment for simulating post-disaster scenarios. In each operation mode, the user has a different method to control the robot. In addition, in order to better simulate the process by which the robot scans its surroundings and the computer side cumulatively gets a reconstructed 3D virtual scene, the test environment was implemented in such a way that the picture seen by the user depends on the direction of the robot's movement and the trajectory it travels through.



## 2. System Architecture

> - Compurter Information: CPU,GPU
>
> - HTC Vive（半页）
>
> - ROS and Robot
>
> - Unity VR engine & SteamVR
>
> - [UML/ Relationship photo]
>
> 	

The proposed system runs on a computer with the Windows 10 operating system. This computer has been equipped with an Intel Core i7-8700K CPU, 32 GB RAM as well as a NVIDIA GTX 1080 GPU with 8 GB VRAM. HTC Vive is used as a VR device. It has a resolution of 1080 × 1200 per eye, resulting in a total resolution of 2160 × 1200 pixels, a refresh rate of 90 Hz, and a field of view of 110 degrees. It includes two motion controllers and uses two Lighthouses to track the position of the headset as well as the motion controllers.

Unity was chosen as the platform to develop the system. Unity is a widely used game engine with a Steam VR plugin \footnote{https://assetstore.unity.com/packages/tools/integration/steamvr-plugin-32647}, which allows developers to focus on the VR environment and interactive behaviors in programming, rather than specific controller buttons and headset positioning, making VR development much simpler. Another reason why Unity was chosen as a development platform was the potential for collaboration with the Robot Operating System (ROS), a frequently used operating system for robot simulation and manipulation, which is flexible, low-coupling, distributed, open source, and has a powerful and rich third-party feature set. In terms of collaboration between Unity and ROS, Siemens provides open source software libraries and tools in C\# for communicating with ROS from .NET applications \footnote{https://github.com/siemens/ros-sharp}. Combining ROS and Unity to develop a collaborative human-robot interaction platform proved to be feasible\cite{Whitney:2018wk}. Since the focus of this paper is on human-robot interaction, collaboration and synchronization of ROS will not be explored in detail here.

## 3. Interaction techniques

This system has 4 different approaches to control the robot. Each mode has its own distinctive features: 

```latex
\begin{enumerate}
\item In Handle Mode the user will send control commands directly using the motion controller. 
\item In Lab Mode a simulated lab is constructed in the VR environment and the user will use virtual buttons in the lab to control the rescue robot. 
\item In Remote Mode the user can set the driving destination directly. 
\item In UI Mode the user has a virtual menu and sends commands via rays from the motion controller.
\end{enumerate}
```

In order to improve the reusability of the code and to facilitate the management of subsequent development, the classes that manage the interaction actions of each mode implement the same interface. A graphical representation of the system activities workﬂow is given in the UML activity diagram in Fig.\ref{fig:uml}.

```latex
\begin{figure}[h]
    \centering
    \includegraphics[height=14cm]{graphics/uml.png}
    \caption{UML Class diagram for the structure of the system}
    \label{fig:uml}
\end{figure}
```



##### 1. Handle Mode

> - main feature
> - functions: how to move robot, camera, map...
> - photo



##### 2. Lab Mode

> - main feature
> - functions: how to move robot, button, speed editor, auto drive 3 monitor....
> - photo



##### 3. Remote Mode

> - main feature
> - functions: how to move robot: target(Pseudocode?) or virtural joystick. ItemPackage in Steam
> - photo



##### 4. UI Mode

> - main feature
> - functions: introcude here compositions of the UI menu
> - photo





## 4. Test Scene

> - goal of the project: rescue robots  => destroyed city,
>- environment:  destroyed city & Collider for test  [photo] 





# Evaluation of User Experience

> ##### main goal of test (Overview)
>
> - Evaluate user experience and robot performance in different operating modes



> ##### test process
>
> - test group
>
> - task: The user is required to .....
> - what will be recorded: time, collisions ...(?)







# Results

> ##### results and discussion
>
> - Difficulty of learning how to operate
> - Efficiency of operating robots (time)
> - Number of robot collisions (collision)
> - [Table: Test results]





# Conclusion

> ##### What I have done (overview)
>
> ###### Unity Project
>
> - 4 operation modes
>
> - test scenes
>
> 
>
> ###### User Study
>
> - compared and evaluated .....
> - results:  .......



> ##### Future work
>
> - communication with ROS
> - Real Robots
> - Use real scene: reconstructed 3D model based on the daten from robot sensors