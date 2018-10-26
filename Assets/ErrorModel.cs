using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorModel {

    public ErrorModel(float kinect_x_offset, float kinect_y_offset, float kinect_z_offset) {
        this.kinect_x_offset = kinect_x_offset;
        this.kinect_y_offset = kinect_y_offset;
        this.kinect_z_offset = kinect_z_offset;
    }

    private float kinect_x_offset; 
    private float kinect_y_offset;
    private float kinect_z_offset;

    private static float[] x_betas = new float[] {0.63801f, 0.11225f, 0.0000035751f, -0.0040645f, -0.00014951f, 0.000070336f,
                                                    -5.6762f, -0.80153f, -0.0031496f, 0.012996f};
    private static float[] y_betas = new float[] {0.63038f, 0.26496f, 0.0000013279f, 0.015f, 0.000090174f, 0.00033417f,
                                                    -5.9320f, -2.4411f, 0.0031239f, 0.10995f};
    private static float[] z_betas = new float[] {0.00002f, 0.00002f, 0.00000125f, 0.000002f, 0.0000000035f, 0.0000000035f,
                                                    -0.010002f, -0.010002f, -0.0015025f, 1.4515f};

    public float GetErrorStdX(MovingBall m) {
        return ComputeError(m, x_betas);
    }

    public float GetErrorStdY(MovingBall m) {
        return ComputeError(m, y_betas);
    }

    public float GetErrorStdZ(MovingBall m) {
        return ComputeError(m, z_betas);
    }

    private float ComputeError(MovingBall m, float[] betas) {
        float i = m.x_offset - this.kinect_x_offset;
        float j = m.y_offset - this.kinect_y_offset;
        float z = m.z_offset - this.kinect_z_offset;

        float ji = i * j;
        float iz = i * z;
        float zj = z * j;

        float i2 = i * i;
        float j2 = j * j;
        float z2 = z * z;

        float square_vals = betas[0] * j2 + betas[1] * i2 + betas[2] * z2;
        float corr_vals = betas[3] * ji + betas[4] * iz + betas[5] * zj;
        float lin_vals = betas[6] * j + betas[7] * i + betas[8] * z + betas[9];

        return square_vals + corr_vals + lin_vals;
    } 

}   
