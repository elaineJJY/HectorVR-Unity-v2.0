import glob
import os
from numpy.lib.function_base import append
import pandas as pd
import matplotlib.pyplot as plt
import time
import seaborn as sns
import numpy as np
import csv
import joypy


def draw(filename,start):
    file = pd.read_csv(FileName,usecols=[start,start+3,start+6,start+9])
    temp = file.values.tolist()
    file = np.transpose(temp)

    kwargs = {
        "bins": 20,
        "histtype": "stepfilled",
        "alpha": 0.5
    }
    
    fig,ax = plt.subplots(figsize=(10, 7))
    for i in range(0,4):
        ax.hist(file[i], color = colors[i],label=conditions[i], **kwargs)

    ax.set_title(filename)
    ax.legend()
    plt.show()
    
def draw2(filename,start):
    file = pd.read_csv(FileName,usecols=[start,start+3,start+6,start+9])
    temp = file.values.tolist()
    # Draw Stripplot
    plt.figure(figsize=(10,5))
    medianprops = dict(linestyle='-', linewidth=1, color='black')
    f = plt.boxplot(file,patch_artist = True,medianprops=medianprops,labels=conditions)
    
    for box,c in zip(f['boxes'], colors):
        box.set(color='black', linewidth=1)
        box.set_alpha(a)
        box.set( facecolor = c )
    plt.title(filename, fontsize=15)
    plt.ylim(0.5,5.5)
    plt.savefig(filename+".jpg",dpi=300)
    #plt.show()


FileName = "Hector V2 Nutzerstudie.csv"
file = pd.read_csv(FileName)
colors = sns.color_palette()
a = 0.6

conditions = ["Handle","Lab","Remote","UI"]
questions = ["I found it easy to move robot in desired position","I found it easy to concentrate on controlling the robot","I found it easy to perceive the details of the environment"]

start = 5;
for i in range(0,3):
    #draw(questions[i],start+i)
    draw2(questions[i],start+i)




