var Deserializers = {}
Deserializers["UnityEngine.JointSpring"] = function (request, data, root) {
  var i362 = root || request.c( 'UnityEngine.JointSpring' )
  var i363 = data
  i362.spring = i363[0]
  i362.damper = i363[1]
  i362.targetPosition = i363[2]
  return i362
}

Deserializers["UnityEngine.JointMotor"] = function (request, data, root) {
  var i364 = root || request.c( 'UnityEngine.JointMotor' )
  var i365 = data
  i364.m_TargetVelocity = i365[0]
  i364.m_Force = i365[1]
  i364.m_FreeSpin = i365[2]
  return i364
}

Deserializers["UnityEngine.JointLimits"] = function (request, data, root) {
  var i366 = root || request.c( 'UnityEngine.JointLimits' )
  var i367 = data
  i366.m_Min = i367[0]
  i366.m_Max = i367[1]
  i366.m_Bounciness = i367[2]
  i366.m_BounceMinVelocity = i367[3]
  i366.m_ContactDistance = i367[4]
  i366.minBounce = i367[5]
  i366.maxBounce = i367[6]
  return i366
}

Deserializers["UnityEngine.JointDrive"] = function (request, data, root) {
  var i368 = root || request.c( 'UnityEngine.JointDrive' )
  var i369 = data
  i368.m_PositionSpring = i369[0]
  i368.m_PositionDamper = i369[1]
  i368.m_MaximumForce = i369[2]
  i368.m_UseAcceleration = i369[3]
  return i368
}

Deserializers["UnityEngine.SoftJointLimitSpring"] = function (request, data, root) {
  var i370 = root || request.c( 'UnityEngine.SoftJointLimitSpring' )
  var i371 = data
  i370.m_Spring = i371[0]
  i370.m_Damper = i371[1]
  return i370
}

Deserializers["UnityEngine.SoftJointLimit"] = function (request, data, root) {
  var i372 = root || request.c( 'UnityEngine.SoftJointLimit' )
  var i373 = data
  i372.m_Limit = i373[0]
  i372.m_Bounciness = i373[1]
  i372.m_ContactDistance = i373[2]
  return i372
}

Deserializers["UnityEngine.WheelFrictionCurve"] = function (request, data, root) {
  var i374 = root || request.c( 'UnityEngine.WheelFrictionCurve' )
  var i375 = data
  i374.m_ExtremumSlip = i375[0]
  i374.m_ExtremumValue = i375[1]
  i374.m_AsymptoteSlip = i375[2]
  i374.m_AsymptoteValue = i375[3]
  i374.m_Stiffness = i375[4]
  return i374
}

Deserializers["UnityEngine.JointAngleLimits2D"] = function (request, data, root) {
  var i376 = root || request.c( 'UnityEngine.JointAngleLimits2D' )
  var i377 = data
  i376.m_LowerAngle = i377[0]
  i376.m_UpperAngle = i377[1]
  return i376
}

Deserializers["UnityEngine.JointMotor2D"] = function (request, data, root) {
  var i378 = root || request.c( 'UnityEngine.JointMotor2D' )
  var i379 = data
  i378.m_MotorSpeed = i379[0]
  i378.m_MaximumMotorTorque = i379[1]
  return i378
}

Deserializers["UnityEngine.JointSuspension2D"] = function (request, data, root) {
  var i380 = root || request.c( 'UnityEngine.JointSuspension2D' )
  var i381 = data
  i380.m_DampingRatio = i381[0]
  i380.m_Frequency = i381[1]
  i380.m_Angle = i381[2]
  return i380
}

Deserializers["UnityEngine.JointTranslationLimits2D"] = function (request, data, root) {
  var i382 = root || request.c( 'UnityEngine.JointTranslationLimits2D' )
  var i383 = data
  i382.m_LowerTranslation = i383[0]
  i382.m_UpperTranslation = i383[1]
  return i382
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.Transform"] = function (request, data, root) {
  var i384 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.Transform' )
  var i385 = data
  i384.position = new pc.Vec3( i385[0], i385[1], i385[2] )
  i384.scale = new pc.Vec3( i385[3], i385[4], i385[5] )
  i384.rotation = new pc.Quat(i385[6], i385[7], i385[8], i385[9])
  return i384
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.MeshFilter"] = function (request, data, root) {
  var i386 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.MeshFilter' )
  var i387 = data
  request.r(i387[0], i387[1], 0, i386, 'sharedMesh')
  return i386
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.MeshRenderer"] = function (request, data, root) {
  var i388 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.MeshRenderer' )
  var i389 = data
  request.r(i389[0], i389[1], 0, i388, 'additionalVertexStreams')
  i388.enabled = !!i389[2]
  request.r(i389[3], i389[4], 0, i388, 'sharedMaterial')
  var i391 = i389[5]
  var i390 = []
  for(var i = 0; i < i391.length; i += 2) {
  request.r(i391[i + 0], i391[i + 1], 2, i390, '')
  }
  i388.sharedMaterials = i390
  i388.receiveShadows = !!i389[6]
  i388.shadowCastingMode = i389[7]
  i388.sortingLayerID = i389[8]
  i388.sortingOrder = i389[9]
  i388.lightmapIndex = i389[10]
  i388.lightmapSceneIndex = i389[11]
  i388.lightmapScaleOffset = new pc.Vec4( i389[12], i389[13], i389[14], i389[15] )
  i388.lightProbeUsage = i389[16]
  i388.reflectionProbeUsage = i389[17]
  return i388
}

Deserializers["HexCellView"] = function (request, data, root) {
  var i394 = root || request.c( 'HexCellView' )
  var i395 = data
  request.r(i395[0], i395[1], 0, i394, '_renderer')
  return i394
}

Deserializers["Luna.Unity.DTO.UnityEngine.Scene.GameObject"] = function (request, data, root) {
  var i396 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Scene.GameObject' )
  var i397 = data
  i396.name = i397[0]
  i396.tagId = i397[1]
  i396.enabled = !!i397[2]
  i396.isStatic = !!i397[3]
  i396.layer = i397[4]
  return i396
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Mesh"] = function (request, data, root) {
  var i398 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Mesh' )
  var i399 = data
  i398.name = i399[0]
  i398.halfPrecision = !!i399[1]
  i398.useSimplification = !!i399[2]
  i398.useUInt32IndexFormat = !!i399[3]
  i398.vertexCount = i399[4]
  i398.aabb = i399[5]
  var i401 = i399[6]
  var i400 = []
  for(var i = 0; i < i401.length; i += 1) {
    i400.push( !!i401[i + 0] );
  }
  i398.streams = i400
  i398.vertices = i399[7]
  var i403 = i399[8]
  var i402 = []
  for(var i = 0; i < i403.length; i += 1) {
    i402.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Mesh+SubMesh', i403[i + 0]) );
  }
  i398.subMeshes = i402
  var i405 = i399[9]
  var i404 = []
  for(var i = 0; i < i405.length; i += 16) {
    i404.push( new pc.Mat4().setData(i405[i + 0], i405[i + 1], i405[i + 2], i405[i + 3],  i405[i + 4], i405[i + 5], i405[i + 6], i405[i + 7],  i405[i + 8], i405[i + 9], i405[i + 10], i405[i + 11],  i405[i + 12], i405[i + 13], i405[i + 14], i405[i + 15]) );
  }
  i398.bindposes = i404
  var i407 = i399[10]
  var i406 = []
  for(var i = 0; i < i407.length; i += 1) {
    i406.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Mesh+BlendShape', i407[i + 0]) );
  }
  i398.blendShapes = i406
  return i398
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Mesh+SubMesh"] = function (request, data, root) {
  var i412 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Mesh+SubMesh' )
  var i413 = data
  i412.triangles = i413[0]
  return i412
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Mesh+BlendShape"] = function (request, data, root) {
  var i418 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Mesh+BlendShape' )
  var i419 = data
  i418.name = i419[0]
  var i421 = i419[1]
  var i420 = []
  for(var i = 0; i < i421.length; i += 1) {
    i420.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Mesh+BlendShapeFrame', i421[i + 0]) );
  }
  i418.frames = i420
  return i418
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material"] = function (request, data, root) {
  var i422 = root || new pc.UnityMaterial()
  var i423 = data
  i422.name = i423[0]
  request.r(i423[1], i423[2], 0, i422, 'shader')
  i422.renderQueue = i423[3]
  i422.enableInstancing = !!i423[4]
  var i425 = i423[5]
  var i424 = []
  for(var i = 0; i < i425.length; i += 1) {
    i424.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+FloatParameter', i425[i + 0]) );
  }
  i422.floatParameters = i424
  var i427 = i423[6]
  var i426 = []
  for(var i = 0; i < i427.length; i += 1) {
    i426.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+ColorParameter', i427[i + 0]) );
  }
  i422.colorParameters = i426
  var i429 = i423[7]
  var i428 = []
  for(var i = 0; i < i429.length; i += 1) {
    i428.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+VectorParameter', i429[i + 0]) );
  }
  i422.vectorParameters = i428
  var i431 = i423[8]
  var i430 = []
  for(var i = 0; i < i431.length; i += 1) {
    i430.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+TextureParameter', i431[i + 0]) );
  }
  i422.textureParameters = i430
  var i433 = i423[9]
  var i432 = []
  for(var i = 0; i < i433.length; i += 1) {
    i432.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+MaterialFlag', i433[i + 0]) );
  }
  i422.materialFlags = i432
  return i422
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+FloatParameter"] = function (request, data, root) {
  var i436 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+FloatParameter' )
  var i437 = data
  i436.name = i437[0]
  i436.value = i437[1]
  return i436
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+ColorParameter"] = function (request, data, root) {
  var i440 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+ColorParameter' )
  var i441 = data
  i440.name = i441[0]
  i440.value = new pc.Color(i441[1], i441[2], i441[3], i441[4])
  return i440
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+VectorParameter"] = function (request, data, root) {
  var i444 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+VectorParameter' )
  var i445 = data
  i444.name = i445[0]
  i444.value = new pc.Vec4( i445[1], i445[2], i445[3], i445[4] )
  return i444
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+TextureParameter"] = function (request, data, root) {
  var i448 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+TextureParameter' )
  var i449 = data
  i448.name = i449[0]
  request.r(i449[1], i449[2], 0, i448, 'value')
  return i448
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+MaterialFlag"] = function (request, data, root) {
  var i452 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+MaterialFlag' )
  var i453 = data
  i452.name = i453[0]
  i452.enabled = !!i453[1]
  return i452
}

Deserializers["TileStackView"] = function (request, data, root) {
  var i454 = root || request.c( 'TileStackView' )
  var i455 = data
  request.r(i455[0], i455[1], 0, i454, '_renderer')
  request.r(i455[2], i455[3], 0, i454, '_config')
  return i454
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.ParticleSystem"] = function (request, data, root) {
  var i456 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.ParticleSystem' )
  var i457 = data
  i456.main = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.MainModule', i457[0], i456.main)
  i456.colorBySpeed = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ColorBySpeedModule', i457[1], i456.colorBySpeed)
  i456.colorOverLifetime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ColorOverLifetimeModule', i457[2], i456.colorOverLifetime)
  i456.emission = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.EmissionModule', i457[3], i456.emission)
  i456.rotationBySpeed = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.RotationBySpeedModule', i457[4], i456.rotationBySpeed)
  i456.rotationOverLifetime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.RotationOverLifetimeModule', i457[5], i456.rotationOverLifetime)
  i456.shape = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ShapeModule', i457[6], i456.shape)
  i456.sizeBySpeed = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.SizeBySpeedModule', i457[7], i456.sizeBySpeed)
  i456.sizeOverLifetime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.SizeOverLifetimeModule', i457[8], i456.sizeOverLifetime)
  i456.textureSheetAnimation = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.TextureSheetAnimationModule', i457[9], i456.textureSheetAnimation)
  i456.velocityOverLifetime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.VelocityOverLifetimeModule', i457[10], i456.velocityOverLifetime)
  i456.noise = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.NoiseModule', i457[11], i456.noise)
  i456.inheritVelocity = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.InheritVelocityModule', i457[12], i456.inheritVelocity)
  i456.forceOverLifetime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ForceOverLifetimeModule', i457[13], i456.forceOverLifetime)
  i456.limitVelocityOverLifetime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.LimitVelocityOverLifetimeModule', i457[14], i456.limitVelocityOverLifetime)
  i456.useAutoRandomSeed = !!i457[15]
  i456.randomSeed = i457[16]
  return i456
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.MainModule"] = function (request, data, root) {
  var i458 = root || new pc.ParticleSystemMain()
  var i459 = data
  i458.duration = i459[0]
  i458.loop = !!i459[1]
  i458.prewarm = !!i459[2]
  i458.startDelay = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i459[3], i458.startDelay)
  i458.startLifetime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i459[4], i458.startLifetime)
  i458.startSpeed = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i459[5], i458.startSpeed)
  i458.startSize3D = !!i459[6]
  i458.startSizeX = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i459[7], i458.startSizeX)
  i458.startSizeY = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i459[8], i458.startSizeY)
  i458.startSizeZ = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i459[9], i458.startSizeZ)
  i458.startRotation3D = !!i459[10]
  i458.startRotationX = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i459[11], i458.startRotationX)
  i458.startRotationY = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i459[12], i458.startRotationY)
  i458.startRotationZ = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i459[13], i458.startRotationZ)
  i458.startColor = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxGradient', i459[14], i458.startColor)
  i458.gravityModifier = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i459[15], i458.gravityModifier)
  i458.simulationSpace = i459[16]
  request.r(i459[17], i459[18], 0, i458, 'customSimulationSpace')
  i458.simulationSpeed = i459[19]
  i458.useUnscaledTime = !!i459[20]
  i458.scalingMode = i459[21]
  i458.playOnAwake = !!i459[22]
  i458.maxParticles = i459[23]
  i458.emitterVelocityMode = i459[24]
  i458.stopAction = i459[25]
  return i458
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve"] = function (request, data, root) {
  var i460 = root || new pc.MinMaxCurve()
  var i461 = data
  i460.mode = i461[0]
  i460.curveMin = new pc.AnimationCurve( { keys_flow: i461[1] } )
  i460.curveMax = new pc.AnimationCurve( { keys_flow: i461[2] } )
  i460.curveMultiplier = i461[3]
  i460.constantMin = i461[4]
  i460.constantMax = i461[5]
  return i460
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxGradient"] = function (request, data, root) {
  var i462 = root || new pc.MinMaxGradient()
  var i463 = data
  i462.mode = i463[0]
  i462.gradientMin = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Gradient', i463[1], i462.gradientMin)
  i462.gradientMax = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Gradient', i463[2], i462.gradientMax)
  i462.colorMin = new pc.Color(i463[3], i463[4], i463[5], i463[6])
  i462.colorMax = new pc.Color(i463[7], i463[8], i463[9], i463[10])
  return i462
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Gradient"] = function (request, data, root) {
  var i464 = root || request.c( 'Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Gradient' )
  var i465 = data
  i464.mode = i465[0]
  var i467 = i465[1]
  var i466 = []
  for(var i = 0; i < i467.length; i += 1) {
    i466.push( request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientColorKey', i467[i + 0]) );
  }
  i464.colorKeys = i466
  var i469 = i465[2]
  var i468 = []
  for(var i = 0; i < i469.length; i += 1) {
    i468.push( request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientAlphaKey', i469[i + 0]) );
  }
  i464.alphaKeys = i468
  return i464
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ColorBySpeedModule"] = function (request, data, root) {
  var i470 = root || new pc.ParticleSystemColorBySpeed()
  var i471 = data
  i470.enabled = !!i471[0]
  i470.color = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxGradient', i471[1], i470.color)
  i470.range = new pc.Vec2( i471[2], i471[3] )
  return i470
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientColorKey"] = function (request, data, root) {
  var i474 = root || request.c( 'Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientColorKey' )
  var i475 = data
  i474.color = new pc.Color(i475[0], i475[1], i475[2], i475[3])
  i474.time = i475[4]
  return i474
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientAlphaKey"] = function (request, data, root) {
  var i478 = root || request.c( 'Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientAlphaKey' )
  var i479 = data
  i478.alpha = i479[0]
  i478.time = i479[1]
  return i478
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ColorOverLifetimeModule"] = function (request, data, root) {
  var i480 = root || new pc.ParticleSystemColorOverLifetime()
  var i481 = data
  i480.enabled = !!i481[0]
  i480.color = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxGradient', i481[1], i480.color)
  return i480
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.EmissionModule"] = function (request, data, root) {
  var i482 = root || new pc.ParticleSystemEmitter()
  var i483 = data
  i482.enabled = !!i483[0]
  i482.rateOverTime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i483[1], i482.rateOverTime)
  i482.rateOverDistance = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i483[2], i482.rateOverDistance)
  var i485 = i483[3]
  var i484 = []
  for(var i = 0; i < i485.length; i += 1) {
    i484.push( request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Burst', i485[i + 0]) );
  }
  i482.bursts = i484
  return i482
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Burst"] = function (request, data, root) {
  var i488 = root || new pc.ParticleSystemBurst()
  var i489 = data
  i488.count = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i489[0], i488.count)
  i488.cycleCount = i489[1]
  i488.minCount = i489[2]
  i488.maxCount = i489[3]
  i488.repeatInterval = i489[4]
  i488.time = i489[5]
  return i488
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.RotationBySpeedModule"] = function (request, data, root) {
  var i490 = root || new pc.ParticleSystemRotationBySpeed()
  var i491 = data
  i490.enabled = !!i491[0]
  i490.x = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i491[1], i490.x)
  i490.y = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i491[2], i490.y)
  i490.z = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i491[3], i490.z)
  i490.separateAxes = !!i491[4]
  i490.range = new pc.Vec2( i491[5], i491[6] )
  return i490
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.RotationOverLifetimeModule"] = function (request, data, root) {
  var i492 = root || new pc.ParticleSystemRotationOverLifetime()
  var i493 = data
  i492.enabled = !!i493[0]
  i492.x = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i493[1], i492.x)
  i492.y = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i493[2], i492.y)
  i492.z = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i493[3], i492.z)
  i492.separateAxes = !!i493[4]
  return i492
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ShapeModule"] = function (request, data, root) {
  var i494 = root || new pc.ParticleSystemShape()
  var i495 = data
  i494.enabled = !!i495[0]
  i494.shapeType = i495[1]
  i494.randomDirectionAmount = i495[2]
  i494.sphericalDirectionAmount = i495[3]
  i494.randomPositionAmount = i495[4]
  i494.alignToDirection = !!i495[5]
  i494.radius = i495[6]
  i494.radiusMode = i495[7]
  i494.radiusSpread = i495[8]
  i494.radiusSpeed = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i495[9], i494.radiusSpeed)
  i494.radiusThickness = i495[10]
  i494.angle = i495[11]
  i494.length = i495[12]
  i494.boxThickness = new pc.Vec3( i495[13], i495[14], i495[15] )
  i494.meshShapeType = i495[16]
  request.r(i495[17], i495[18], 0, i494, 'mesh')
  request.r(i495[19], i495[20], 0, i494, 'meshRenderer')
  request.r(i495[21], i495[22], 0, i494, 'skinnedMeshRenderer')
  i494.useMeshMaterialIndex = !!i495[23]
  i494.meshMaterialIndex = i495[24]
  i494.useMeshColors = !!i495[25]
  i494.normalOffset = i495[26]
  i494.arc = i495[27]
  i494.arcMode = i495[28]
  i494.arcSpread = i495[29]
  i494.arcSpeed = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i495[30], i494.arcSpeed)
  i494.donutRadius = i495[31]
  i494.position = new pc.Vec3( i495[32], i495[33], i495[34] )
  i494.rotation = new pc.Vec3( i495[35], i495[36], i495[37] )
  i494.scale = new pc.Vec3( i495[38], i495[39], i495[40] )
  return i494
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.SizeBySpeedModule"] = function (request, data, root) {
  var i496 = root || new pc.ParticleSystemSizeBySpeed()
  var i497 = data
  i496.enabled = !!i497[0]
  i496.x = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i497[1], i496.x)
  i496.y = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i497[2], i496.y)
  i496.z = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i497[3], i496.z)
  i496.separateAxes = !!i497[4]
  i496.range = new pc.Vec2( i497[5], i497[6] )
  return i496
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.SizeOverLifetimeModule"] = function (request, data, root) {
  var i498 = root || new pc.ParticleSystemSizeOverLifetime()
  var i499 = data
  i498.enabled = !!i499[0]
  i498.x = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i499[1], i498.x)
  i498.y = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i499[2], i498.y)
  i498.z = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i499[3], i498.z)
  i498.separateAxes = !!i499[4]
  return i498
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.TextureSheetAnimationModule"] = function (request, data, root) {
  var i500 = root || new pc.ParticleSystemTextureSheetAnimation()
  var i501 = data
  i500.enabled = !!i501[0]
  i500.mode = i501[1]
  i500.animation = i501[2]
  i500.numTilesX = i501[3]
  i500.numTilesY = i501[4]
  i500.useRandomRow = !!i501[5]
  i500.frameOverTime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i501[6], i500.frameOverTime)
  i500.startFrame = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i501[7], i500.startFrame)
  i500.cycleCount = i501[8]
  i500.rowIndex = i501[9]
  i500.flipU = i501[10]
  i500.flipV = i501[11]
  i500.spriteCount = i501[12]
  var i503 = i501[13]
  var i502 = []
  for(var i = 0; i < i503.length; i += 2) {
  request.r(i503[i + 0], i503[i + 1], 2, i502, '')
  }
  i500.sprites = i502
  return i500
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.VelocityOverLifetimeModule"] = function (request, data, root) {
  var i506 = root || new pc.ParticleSystemVelocityOverLifetime()
  var i507 = data
  i506.enabled = !!i507[0]
  i506.x = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i507[1], i506.x)
  i506.y = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i507[2], i506.y)
  i506.z = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i507[3], i506.z)
  i506.radial = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i507[4], i506.radial)
  i506.speedModifier = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i507[5], i506.speedModifier)
  i506.space = i507[6]
  i506.orbitalX = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i507[7], i506.orbitalX)
  i506.orbitalY = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i507[8], i506.orbitalY)
  i506.orbitalZ = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i507[9], i506.orbitalZ)
  i506.orbitalOffsetX = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i507[10], i506.orbitalOffsetX)
  i506.orbitalOffsetY = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i507[11], i506.orbitalOffsetY)
  i506.orbitalOffsetZ = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i507[12], i506.orbitalOffsetZ)
  return i506
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.NoiseModule"] = function (request, data, root) {
  var i508 = root || new pc.ParticleSystemNoise()
  var i509 = data
  i508.enabled = !!i509[0]
  i508.separateAxes = !!i509[1]
  i508.strengthX = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i509[2], i508.strengthX)
  i508.strengthY = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i509[3], i508.strengthY)
  i508.strengthZ = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i509[4], i508.strengthZ)
  i508.frequency = i509[5]
  i508.damping = !!i509[6]
  i508.octaveCount = i509[7]
  i508.octaveMultiplier = i509[8]
  i508.octaveScale = i509[9]
  i508.quality = i509[10]
  i508.scrollSpeed = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i509[11], i508.scrollSpeed)
  i508.scrollSpeedMultiplier = i509[12]
  i508.remapEnabled = !!i509[13]
  i508.remapX = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i509[14], i508.remapX)
  i508.remapY = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i509[15], i508.remapY)
  i508.remapZ = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i509[16], i508.remapZ)
  i508.positionAmount = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i509[17], i508.positionAmount)
  i508.rotationAmount = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i509[18], i508.rotationAmount)
  i508.sizeAmount = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i509[19], i508.sizeAmount)
  return i508
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.InheritVelocityModule"] = function (request, data, root) {
  var i510 = root || new pc.ParticleSystemInheritVelocity()
  var i511 = data
  i510.enabled = !!i511[0]
  i510.mode = i511[1]
  i510.curve = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i511[2], i510.curve)
  return i510
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ForceOverLifetimeModule"] = function (request, data, root) {
  var i512 = root || new pc.ParticleSystemForceOverLifetime()
  var i513 = data
  i512.enabled = !!i513[0]
  i512.x = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i513[1], i512.x)
  i512.y = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i513[2], i512.y)
  i512.z = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i513[3], i512.z)
  i512.space = i513[4]
  i512.randomized = !!i513[5]
  return i512
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.LimitVelocityOverLifetimeModule"] = function (request, data, root) {
  var i514 = root || new pc.ParticleSystemLimitVelocityOverLifetime()
  var i515 = data
  i514.enabled = !!i515[0]
  i514.limit = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i515[1], i514.limit)
  i514.limitX = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i515[2], i514.limitX)
  i514.limitY = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i515[3], i514.limitY)
  i514.limitZ = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i515[4], i514.limitZ)
  i514.dampen = i515[5]
  i514.separateAxes = !!i515[6]
  i514.space = i515[7]
  i514.drag = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i515[8], i514.drag)
  i514.multiplyDragByParticleSize = !!i515[9]
  i514.multiplyDragByParticleVelocity = !!i515[10]
  return i514
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.ParticleSystemRenderer"] = function (request, data, root) {
  var i516 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.ParticleSystemRenderer' )
  var i517 = data
  request.r(i517[0], i517[1], 0, i516, 'mesh')
  i516.meshCount = i517[2]
  i516.activeVertexStreamsCount = i517[3]
  i516.alignment = i517[4]
  i516.renderMode = i517[5]
  i516.sortMode = i517[6]
  i516.lengthScale = i517[7]
  i516.velocityScale = i517[8]
  i516.cameraVelocityScale = i517[9]
  i516.normalDirection = i517[10]
  i516.sortingFudge = i517[11]
  i516.minParticleSize = i517[12]
  i516.maxParticleSize = i517[13]
  i516.pivot = new pc.Vec3( i517[14], i517[15], i517[16] )
  request.r(i517[17], i517[18], 0, i516, 'trailMaterial')
  i516.applyActiveColorSpace = !!i517[19]
  i516.enabled = !!i517[20]
  request.r(i517[21], i517[22], 0, i516, 'sharedMaterial')
  var i519 = i517[23]
  var i518 = []
  for(var i = 0; i < i519.length; i += 2) {
  request.r(i519[i + 0], i519[i + 1], 2, i518, '')
  }
  i516.sharedMaterials = i518
  i516.receiveShadows = !!i517[24]
  i516.shadowCastingMode = i517[25]
  i516.sortingLayerID = i517[26]
  i516.sortingOrder = i517[27]
  i516.lightmapIndex = i517[28]
  i516.lightmapSceneIndex = i517[29]
  i516.lightmapScaleOffset = new pc.Vec4( i517[30], i517[31], i517[32], i517[33] )
  i516.lightProbeUsage = i517[34]
  i516.reflectionProbeUsage = i517[35]
  return i516
}

Deserializers["Luna.Unity.DTO.UnityEngine.Textures.Texture2D"] = function (request, data, root) {
  var i520 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Textures.Texture2D' )
  var i521 = data
  i520.name = i521[0]
  i520.width = i521[1]
  i520.height = i521[2]
  i520.mipmapCount = i521[3]
  i520.anisoLevel = i521[4]
  i520.filterMode = i521[5]
  i520.hdr = !!i521[6]
  i520.format = i521[7]
  i520.wrapMode = i521[8]
  i520.alphaIsTransparency = !!i521[9]
  i520.alphaSource = i521[10]
  i520.graphicsFormat = i521[11]
  i520.sRGBTexture = !!i521[12]
  i520.desiredColorSpace = i521[13]
  i520.wrapU = i521[14]
  i520.wrapV = i521[15]
  return i520
}

Deserializers["TileStackRoot"] = function (request, data, root) {
  var i522 = root || request.c( 'TileStackRoot' )
  var i523 = data
  request.r(i523[0], i523[1], 0, i522, '_tileStackView')
  i522._dragLerpSpeed = i523[2]
  return i522
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.SphereCollider"] = function (request, data, root) {
  var i524 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.SphereCollider' )
  var i525 = data
  i524.center = new pc.Vec3( i525[0], i525[1], i525[2] )
  i524.radius = i525[3]
  i524.enabled = !!i525[4]
  i524.isTrigger = !!i525[5]
  request.r(i525[6], i525[7], 0, i524, 'material')
  return i524
}

Deserializers["Luna.Unity.DTO.UnityEngine.Textures.Cubemap"] = function (request, data, root) {
  var i526 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Textures.Cubemap' )
  var i527 = data
  i526.name = i527[0]
  i526.atlasId = i527[1]
  i526.mipmapCount = i527[2]
  i526.hdr = !!i527[3]
  i526.size = i527[4]
  i526.anisoLevel = i527[5]
  i526.filterMode = i527[6]
  var i529 = i527[7]
  var i528 = []
  for(var i = 0; i < i529.length; i += 4) {
    i528.push( UnityEngine.Rect.MinMaxRect(i529[i + 0], i529[i + 1], i529[i + 2], i529[i + 3]) );
  }
  i526.rects = i528
  i526.wrapU = i527[8]
  i526.wrapV = i527[9]
  return i526
}

Deserializers["Luna.Unity.DTO.UnityEngine.Scene.Scene"] = function (request, data, root) {
  var i532 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Scene.Scene' )
  var i533 = data
  i532.name = i533[0]
  i532.index = i533[1]
  i532.startup = !!i533[2]
  return i532
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.Camera"] = function (request, data, root) {
  var i534 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.Camera' )
  var i535 = data
  i534.aspect = i535[0]
  i534.orthographic = !!i535[1]
  i534.orthographicSize = i535[2]
  i534.backgroundColor = new pc.Color(i535[3], i535[4], i535[5], i535[6])
  i534.nearClipPlane = i535[7]
  i534.farClipPlane = i535[8]
  i534.fieldOfView = i535[9]
  i534.depth = i535[10]
  i534.clearFlags = i535[11]
  i534.cullingMask = i535[12]
  i534.rect = i535[13]
  request.r(i535[14], i535[15], 0, i534, 'targetTexture')
  i534.usePhysicalProperties = !!i535[16]
  i534.focalLength = i535[17]
  i534.sensorSize = new pc.Vec2( i535[18], i535[19] )
  i534.lensShift = new pc.Vec2( i535[20], i535[21] )
  i534.gateFit = i535[22]
  i534.commandBufferCount = i535[23]
  i534.cameraType = i535[24]
  i534.enabled = !!i535[25]
  return i534
}

Deserializers["UnityEngine.Rendering.Universal.UniversalAdditionalCameraData"] = function (request, data, root) {
  var i536 = root || request.c( 'UnityEngine.Rendering.Universal.UniversalAdditionalCameraData' )
  var i537 = data
  i536.m_RenderShadows = !!i537[0]
  i536.m_RequiresDepthTextureOption = i537[1]
  i536.m_RequiresOpaqueTextureOption = i537[2]
  i536.m_CameraType = i537[3]
  var i539 = i537[4]
  var i538 = new (System.Collections.Generic.List$1(Bridge.ns('UnityEngine.Camera')))
  for(var i = 0; i < i539.length; i += 2) {
  request.r(i539[i + 0], i539[i + 1], 1, i538, '')
  }
  i536.m_Cameras = i538
  i536.m_RendererIndex = i537[5]
  i536.m_VolumeLayerMask = UnityEngine.LayerMask.FromIntegerValue( i537[6] )
  request.r(i537[7], i537[8], 0, i536, 'm_VolumeTrigger')
  i536.m_VolumeFrameworkUpdateModeOption = i537[9]
  i536.m_RenderPostProcessing = !!i537[10]
  i536.m_Antialiasing = i537[11]
  i536.m_AntialiasingQuality = i537[12]
  i536.m_StopNaN = !!i537[13]
  i536.m_Dithering = !!i537[14]
  i536.m_ClearDepth = !!i537[15]
  i536.m_AllowXRRendering = !!i537[16]
  i536.m_AllowHDROutput = !!i537[17]
  i536.m_UseScreenCoordOverride = !!i537[18]
  i536.m_ScreenSizeOverride = new pc.Vec4( i537[19], i537[20], i537[21], i537[22] )
  i536.m_ScreenCoordScaleBias = new pc.Vec4( i537[23], i537[24], i537[25], i537[26] )
  i536.m_RequiresDepthTexture = !!i537[27]
  i536.m_RequiresColorTexture = !!i537[28]
  i536.m_Version = i537[29]
  i536.m_TaaSettings = request.d('UnityEngine.Rendering.Universal.TemporalAA+Settings', i537[30], i536.m_TaaSettings)
  return i536
}

Deserializers["UnityEngine.Rendering.Universal.TemporalAA+Settings"] = function (request, data, root) {
  var i542 = root || request.c( 'UnityEngine.Rendering.Universal.TemporalAA+Settings' )
  var i543 = data
  i542.m_Quality = i543[0]
  i542.m_FrameInfluence = i543[1]
  i542.m_JitterScale = i543[2]
  i542.m_MipBias = i543[3]
  i542.m_VarianceClampScale = i543[4]
  i542.m_ContrastAdaptiveSharpening = i543[5]
  return i542
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.Light"] = function (request, data, root) {
  var i544 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.Light' )
  var i545 = data
  i544.type = i545[0]
  i544.color = new pc.Color(i545[1], i545[2], i545[3], i545[4])
  i544.cullingMask = i545[5]
  i544.intensity = i545[6]
  i544.range = i545[7]
  i544.spotAngle = i545[8]
  i544.shadows = i545[9]
  i544.shadowNormalBias = i545[10]
  i544.shadowBias = i545[11]
  i544.shadowStrength = i545[12]
  i544.shadowResolution = i545[13]
  i544.lightmapBakeType = i545[14]
  i544.renderMode = i545[15]
  request.r(i545[16], i545[17], 0, i544, 'cookie')
  i544.cookieSize = i545[18]
  i544.shadowNearPlane = i545[19]
  i544.enabled = !!i545[20]
  return i544
}

Deserializers["UnityEngine.Rendering.Universal.UniversalAdditionalLightData"] = function (request, data, root) {
  var i546 = root || request.c( 'UnityEngine.Rendering.Universal.UniversalAdditionalLightData' )
  var i547 = data
  i546.m_Version = i547[0]
  i546.m_UsePipelineSettings = !!i547[1]
  i546.m_AdditionalLightsShadowResolutionTier = i547[2]
  i546.m_LightLayerMask = i547[3]
  i546.m_RenderingLayers = i547[4]
  i546.m_CustomShadowLayers = !!i547[5]
  i546.m_ShadowLayerMask = i547[6]
  i546.m_ShadowRenderingLayers = i547[7]
  i546.m_LightCookieSize = new pc.Vec2( i547[8], i547[9] )
  i546.m_LightCookieOffset = new pc.Vec2( i547[10], i547[11] )
  i546.m_SoftShadowQuality = i547[12]
  return i546
}

Deserializers["GameplayEntry"] = function (request, data, root) {
  var i548 = root || request.c( 'GameplayEntry' )
  var i549 = data
  request.r(i549[0], i549[1], 0, i548, '_grid')
  request.r(i549[2], i549[3], 0, i548, '_cellPrefab')
  request.r(i549[4], i549[5], 0, i548, '_cellStackPrefab')
  request.r(i549[6], i549[7], 0, i548, '_spawnStackRootPrefab')
  var i551 = i549[8]
  var i550 = []
  for(var i = 0; i < i551.length; i += 2) {
  request.r(i551[i + 0], i551[i + 1], 2, i550, '')
  }
  i548._spawnPoints = i550
  request.r(i549[9], i549[10], 0, i548, '_dragInput')
  request.r(i549[11], i549[12], 0, i548, '_tileConfig')
  request.r(i549[13], i549[14], 0, i548, '_gridDefinitionConfig')
  return i548
}

Deserializers["TileStackDragInput"] = function (request, data, root) {
  var i554 = root || request.c( 'TileStackDragInput' )
  var i555 = data
  request.r(i555[0], i555[1], 0, i554, '_camera')
  i554._stackLayerMask = UnityEngine.LayerMask.FromIntegerValue( i555[2] )
  i554._maxCellPickDistance = i555[3]
  return i554
}

Deserializers["TileStackSpawnPoint"] = function (request, data, root) {
  var i556 = root || request.c( 'TileStackSpawnPoint' )
  var i557 = data
  return i556
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.RenderSettings"] = function (request, data, root) {
  var i558 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.RenderSettings' )
  var i559 = data
  i558.ambientIntensity = i559[0]
  i558.reflectionIntensity = i559[1]
  i558.ambientMode = i559[2]
  i558.ambientLight = new pc.Color(i559[3], i559[4], i559[5], i559[6])
  i558.ambientSkyColor = new pc.Color(i559[7], i559[8], i559[9], i559[10])
  i558.ambientGroundColor = new pc.Color(i559[11], i559[12], i559[13], i559[14])
  i558.ambientEquatorColor = new pc.Color(i559[15], i559[16], i559[17], i559[18])
  i558.fogColor = new pc.Color(i559[19], i559[20], i559[21], i559[22])
  i558.fogEndDistance = i559[23]
  i558.fogStartDistance = i559[24]
  i558.fogDensity = i559[25]
  i558.fog = !!i559[26]
  request.r(i559[27], i559[28], 0, i558, 'skybox')
  i558.fogMode = i559[29]
  var i561 = i559[30]
  var i560 = []
  for(var i = 0; i < i561.length; i += 1) {
    i560.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+Lightmap', i561[i + 0]) );
  }
  i558.lightmaps = i560
  i558.lightProbes = request.d('Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+LightProbes', i559[31], i558.lightProbes)
  i558.lightmapsMode = i559[32]
  i558.mixedBakeMode = i559[33]
  i558.environmentLightingMode = i559[34]
  i558.ambientProbe = new pc.SphericalHarmonicsL2(i559[35])
  i558.referenceAmbientProbe = new pc.SphericalHarmonicsL2(i559[36])
  i558.useReferenceAmbientProbe = !!i559[37]
  request.r(i559[38], i559[39], 0, i558, 'customReflection')
  request.r(i559[40], i559[41], 0, i558, 'defaultReflection')
  i558.defaultReflectionMode = i559[42]
  i558.defaultReflectionResolution = i559[43]
  i558.sunLightObjectId = i559[44]
  i558.pixelLightCount = i559[45]
  i558.defaultReflectionHDR = !!i559[46]
  i558.hasLightDataAsset = !!i559[47]
  i558.hasManualGenerate = !!i559[48]
  return i558
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+Lightmap"] = function (request, data, root) {
  var i564 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+Lightmap' )
  var i565 = data
  request.r(i565[0], i565[1], 0, i564, 'lightmapColor')
  request.r(i565[2], i565[3], 0, i564, 'lightmapDirection')
  return i564
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+LightProbes"] = function (request, data, root) {
  var i566 = root || new UnityEngine.LightProbes()
  var i567 = data
  return i566
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.UniversalRenderPipelineAsset"] = function (request, data, root) {
  var i574 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.UniversalRenderPipelineAsset' )
  var i575 = data
  i574.AdditionalLightsPerObjectLimit = i575[0]
  i574.AdditionalLightsRenderingMode = i575[1]
  i574.LightRenderingMode = request.d('Luna.Unity.DTO.UnityEngine.Assets.LightRenderingMode', i575[2], i574.LightRenderingMode)
  i574.ColorGradingLutSize = i575[3]
  i574.ColorGradingMode = request.d('Luna.Unity.DTO.UnityEngine.Assets.ColorGradingMode', i575[4], i574.ColorGradingMode)
  i574.MainLightRenderingMode = request.d('Luna.Unity.DTO.UnityEngine.Assets.LightRenderingMode', i575[5], i574.MainLightRenderingMode)
  i574.MainLightRenderingModeValue = i575[6]
  i574.SupportsMainLightShadows = !!i575[7]
  i574.MixedLightingSupported = !!i575[8]
  i574.MsaaQuality = request.d('Luna.Unity.DTO.UnityEngine.Assets.MsaaQuality', i575[9], i574.MsaaQuality)
  i574.MSAA = i575[10]
  i574.OpaqueDownsampling = request.d('Luna.Unity.DTO.UnityEngine.Assets.Downsampling', i575[11], i574.OpaqueDownsampling)
  i574.MainLightShadowmapResolution = request.d('Luna.Unity.DTO.UnityEngine.Assets.ShadowResolution', i575[12], i574.MainLightShadowmapResolution)
  i574.MainLightShadowmapResolutionValue = i575[13]
  i574.SupportsSoftShadows = !!i575[14]
  i574.SoftShadowQuality = request.d('Luna.Unity.DTO.UnityEngine.Assets.SoftShadowQuality', i575[15], i574.SoftShadowQuality)
  i574.SoftShadowQualityValue = i575[16]
  i574.ShadowDistance = i575[17]
  i574.ShadowCascadeCount = i575[18]
  i574.Cascade2Split = i575[19]
  i574.Cascade3Split = new pc.Vec2( i575[20], i575[21] )
  i574.Cascade4Split = new pc.Vec3( i575[22], i575[23], i575[24] )
  i574.CascadeBorder = i575[25]
  i574.ShadowDepthBias = i575[26]
  i574.ShadowNormalBias = i575[27]
  i574.RenderScale = i575[28]
  i574.RequireDepthTexture = !!i575[29]
  i574.RequireOpaqueTexture = !!i575[30]
  i574.SupportsHDR = !!i575[31]
  i574.SupportsTerrainHoles = !!i575[32]
  return i574
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.LightRenderingMode"] = function (request, data, root) {
  var i576 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.LightRenderingMode' )
  var i577 = data
  i576.Disabled = i577[0]
  i576.PerVertex = i577[1]
  i576.PerPixel = i577[2]
  return i576
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ColorGradingMode"] = function (request, data, root) {
  var i578 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ColorGradingMode' )
  var i579 = data
  i578.LowDynamicRange = i579[0]
  i578.HighDynamicRange = i579[1]
  return i578
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.MsaaQuality"] = function (request, data, root) {
  var i580 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.MsaaQuality' )
  var i581 = data
  i580.Disabled = i581[0]
  i580._2x = i581[1]
  i580._4x = i581[2]
  i580._8x = i581[3]
  return i580
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Downsampling"] = function (request, data, root) {
  var i582 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Downsampling' )
  var i583 = data
  i582.None = i583[0]
  i582._2xBilinear = i583[1]
  i582._4xBox = i583[2]
  i582._4xBilinear = i583[3]
  return i582
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ShadowResolution"] = function (request, data, root) {
  var i584 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ShadowResolution' )
  var i585 = data
  i584._256 = i585[0]
  i584._512 = i585[1]
  i584._1024 = i585[2]
  i584._2048 = i585[3]
  i584._4096 = i585[4]
  return i584
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.SoftShadowQuality"] = function (request, data, root) {
  var i586 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.SoftShadowQuality' )
  var i587 = data
  i586.UsePipelineSettings = i587[0]
  i586.Low = i587[1]
  i586.Medium = i587[2]
  i586.High = i587[3]
  return i586
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader"] = function (request, data, root) {
  var i588 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader' )
  var i589 = data
  var i591 = i589[0]
  var i590 = new (System.Collections.Generic.List$1(Bridge.ns('Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError')))
  for(var i = 0; i < i591.length; i += 1) {
    i590.add(request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError', i591[i + 0]));
  }
  i588.ShaderCompilationErrors = i590
  i588.name = i589[1]
  i588.guid = i589[2]
  var i593 = i589[3]
  var i592 = []
  for(var i = 0; i < i593.length; i += 1) {
    i592.push( i593[i + 0] );
  }
  i588.shaderDefinedKeywords = i592
  var i595 = i589[4]
  var i594 = []
  for(var i = 0; i < i595.length; i += 1) {
    i594.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass', i595[i + 0]) );
  }
  i588.passes = i594
  var i597 = i589[5]
  var i596 = []
  for(var i = 0; i < i597.length; i += 1) {
    i596.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+UsePass', i597[i + 0]) );
  }
  i588.usePasses = i596
  var i599 = i589[6]
  var i598 = []
  for(var i = 0; i < i599.length; i += 1) {
    i598.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+DefaultParameterValue', i599[i + 0]) );
  }
  i588.defaultParameterValues = i598
  request.r(i589[7], i589[8], 0, i588, 'unityFallbackShader')
  i588.readDepth = !!i589[9]
  i588.hasDepthOnlyPass = !!i589[10]
  i588.isCreatedByShaderGraph = !!i589[11]
  i588.disableBatching = !!i589[12]
  i588.compiled = !!i589[13]
  return i588
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError"] = function (request, data, root) {
  var i602 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError' )
  var i603 = data
  i602.shaderName = i603[0]
  i602.errorMessage = i603[1]
  return i602
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass"] = function (request, data, root) {
  var i608 = root || new pc.UnityShaderPass()
  var i609 = data
  i608.id = i609[0]
  i608.subShaderIndex = i609[1]
  i608.name = i609[2]
  i608.passType = i609[3]
  i608.grabPassTextureName = i609[4]
  i608.usePass = !!i609[5]
  i608.zTest = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i609[6], i608.zTest)
  i608.zWrite = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i609[7], i608.zWrite)
  i608.culling = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i609[8], i608.culling)
  i608.blending = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending', i609[9], i608.blending)
  i608.alphaBlending = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending', i609[10], i608.alphaBlending)
  i608.colorWriteMask = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i609[11], i608.colorWriteMask)
  i608.offsetUnits = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i609[12], i608.offsetUnits)
  i608.offsetFactor = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i609[13], i608.offsetFactor)
  i608.stencilRef = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i609[14], i608.stencilRef)
  i608.stencilReadMask = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i609[15], i608.stencilReadMask)
  i608.stencilWriteMask = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i609[16], i608.stencilWriteMask)
  i608.stencilOp = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp', i609[17], i608.stencilOp)
  i608.stencilOpFront = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp', i609[18], i608.stencilOpFront)
  i608.stencilOpBack = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp', i609[19], i608.stencilOpBack)
  var i611 = i609[20]
  var i610 = []
  for(var i = 0; i < i611.length; i += 1) {
    i610.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Tag', i611[i + 0]) );
  }
  i608.tags = i610
  var i613 = i609[21]
  var i612 = []
  for(var i = 0; i < i613.length; i += 1) {
    i612.push( i613[i + 0] );
  }
  i608.passDefinedKeywords = i612
  var i615 = i609[22]
  var i614 = []
  for(var i = 0; i < i615.length; i += 1) {
    i614.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+KeywordGroup', i615[i + 0]) );
  }
  i608.passDefinedKeywordGroups = i614
  var i617 = i609[23]
  var i616 = []
  for(var i = 0; i < i617.length; i += 1) {
    i616.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant', i617[i + 0]) );
  }
  i608.variants = i616
  var i619 = i609[24]
  var i618 = []
  for(var i = 0; i < i619.length; i += 1) {
    i618.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant', i619[i + 0]) );
  }
  i608.excludedVariants = i618
  i608.hasDepthReader = !!i609[25]
  return i608
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value"] = function (request, data, root) {
  var i620 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value' )
  var i621 = data
  i620.val = i621[0]
  i620.name = i621[1]
  return i620
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending"] = function (request, data, root) {
  var i622 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending' )
  var i623 = data
  i622.src = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i623[0], i622.src)
  i622.dst = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i623[1], i622.dst)
  i622.op = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i623[2], i622.op)
  return i622
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp"] = function (request, data, root) {
  var i624 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp' )
  var i625 = data
  i624.pass = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i625[0], i624.pass)
  i624.fail = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i625[1], i624.fail)
  i624.zFail = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i625[2], i624.zFail)
  i624.comp = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i625[3], i624.comp)
  return i624
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Tag"] = function (request, data, root) {
  var i628 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Tag' )
  var i629 = data
  i628.name = i629[0]
  i628.value = i629[1]
  return i628
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+KeywordGroup"] = function (request, data, root) {
  var i632 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+KeywordGroup' )
  var i633 = data
  var i635 = i633[0]
  var i634 = []
  for(var i = 0; i < i635.length; i += 1) {
    i634.push( i635[i + 0] );
  }
  i632.keywords = i634
  i632.hasDiscard = !!i633[1]
  return i632
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant"] = function (request, data, root) {
  var i638 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant' )
  var i639 = data
  i638.passId = i639[0]
  i638.subShaderIndex = i639[1]
  var i641 = i639[2]
  var i640 = []
  for(var i = 0; i < i641.length; i += 1) {
    i640.push( i641[i + 0] );
  }
  i638.keywords = i640
  i638.vertexProgram = i639[3]
  i638.fragmentProgram = i639[4]
  i638.exportedForWebGl2 = !!i639[5]
  i638.readDepth = !!i639[6]
  return i638
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+UsePass"] = function (request, data, root) {
  var i644 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+UsePass' )
  var i645 = data
  request.r(i645[0], i645[1], 0, i644, 'shader')
  i644.pass = i645[2]
  return i644
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+DefaultParameterValue"] = function (request, data, root) {
  var i648 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+DefaultParameterValue' )
  var i649 = data
  i648.name = i649[0]
  i648.type = i649[1]
  i648.value = new pc.Vec4( i649[2], i649[3], i649[4], i649[5] )
  i648.textureValue = i649[6]
  i648.shaderPropertyFlag = i649[7]
  return i648
}

Deserializers["TileConfig"] = function (request, data, root) {
  var i650 = root || request.c( 'TileConfig' )
  var i651 = data
  var i653 = i651[0]
  var i652 = []
  for(var i = 0; i < i653.length; i += 4) {
    i652.push( new pc.Color(i653[i + 0], i653[i + 1], i653[i + 2], i653[i + 3]) );
  }
  i650._colors = i652
  i650._maxStackSize = i651[1]
  i650._minGeneratedStackSize = i651[2]
  i650._maxGeneratedStackSize = i651[3]
  i650._minClusterSize = i651[4]
  i650._maxClusterSize = i651[5]
  i650._minColorBlocksPerStack = i651[6]
  i650._maxColorBlocksPerStack = i651[7]
  i650._maxColorPickAttempts = i651[8]
  i650._minTilesPerColorBlock = i651[9]
  i650._minTilesPerColorSpawn = i651[10]
  i650._maxTilesPerColorSpawn = i651[11]
  i650._maxStackDistributionIterations = i651[12]
  i650._maxColorsPerSpawnStack = i651[13]
  i650._oneColorStackWeightPercent = i651[14]
  i650._twoColorsStackWeightPercent = i651[15]
  i650._threeColorsStackWeightPercent = i651[16]
  i650._xzScaleFactor = i651[17]
  i650._stackHeight = i651[18]
  i650._yOffset = i651[19]
  i650._spawnXzWorldSize = i651[20]
  i650._spawnStackWorldHeight = i651[21]
  i650._spawnYOffset = i651[22]
  i650._segmentHeight = i651[23]
  i650._segmentGap = i651[24]
  request.r(i651[25], i651[26], 0, i650, '_tileClearEffectPrefab')
  request.r(i651[27], i651[28], 0, i650, '_stackClearEffectPrefab')
  i650._tileClearEffectPrewarmCount = i651[29]
  i650._stackClearEffectPrewarmCount = i651[30]
  return i650
}

Deserializers["GridDefinitionConfig"] = function (request, data, root) {
  var i656 = root || request.c( 'GridDefinitionConfig' )
  var i657 = data
  i656._shapeType = i657[0]
  i656._width = i657[1]
  i656._height = i657[2]
  i656._hexRadius = i657[3]
  i656._occupiedCellsCount = i657[4]
  return i656
}

Deserializers["DG.Tweening.Core.DOTweenSettings"] = function (request, data, root) {
  var i658 = root || request.c( 'DG.Tweening.Core.DOTweenSettings' )
  var i659 = data
  i658.useSafeMode = !!i659[0]
  i658.safeModeOptions = request.d('DG.Tweening.Core.DOTweenSettings+SafeModeOptions', i659[1], i658.safeModeOptions)
  i658.timeScale = i659[2]
  i658.unscaledTimeScale = i659[3]
  i658.useSmoothDeltaTime = !!i659[4]
  i658.maxSmoothUnscaledTime = i659[5]
  i658.rewindCallbackMode = i659[6]
  i658.showUnityEditorReport = !!i659[7]
  i658.logBehaviour = i659[8]
  i658.drawGizmos = !!i659[9]
  i658.defaultRecyclable = !!i659[10]
  i658.defaultAutoPlay = i659[11]
  i658.defaultUpdateType = i659[12]
  i658.defaultTimeScaleIndependent = !!i659[13]
  i658.defaultEaseType = i659[14]
  i658.defaultEaseOvershootOrAmplitude = i659[15]
  i658.defaultEasePeriod = i659[16]
  i658.defaultAutoKill = !!i659[17]
  i658.defaultLoopType = i659[18]
  i658.debugMode = !!i659[19]
  i658.debugStoreTargetId = !!i659[20]
  i658.showPreviewPanel = !!i659[21]
  i658.storeSettingsLocation = i659[22]
  i658.modules = request.d('DG.Tweening.Core.DOTweenSettings+ModulesSetup', i659[23], i658.modules)
  i658.createASMDEF = !!i659[24]
  i658.showPlayingTweens = !!i659[25]
  i658.showPausedTweens = !!i659[26]
  return i658
}

Deserializers["DG.Tweening.Core.DOTweenSettings+SafeModeOptions"] = function (request, data, root) {
  var i660 = root || request.c( 'DG.Tweening.Core.DOTweenSettings+SafeModeOptions' )
  var i661 = data
  i660.logBehaviour = i661[0]
  i660.nestedTweenFailureBehaviour = i661[1]
  return i660
}

Deserializers["DG.Tweening.Core.DOTweenSettings+ModulesSetup"] = function (request, data, root) {
  var i662 = root || request.c( 'DG.Tweening.Core.DOTweenSettings+ModulesSetup' )
  var i663 = data
  i662.showPanel = !!i663[0]
  i662.audioEnabled = !!i663[1]
  i662.physicsEnabled = !!i663[2]
  i662.physics2DEnabled = !!i663[3]
  i662.spriteEnabled = !!i663[4]
  i662.uiEnabled = !!i663[5]
  i662.textMeshProEnabled = !!i663[6]
  i662.tk2DEnabled = !!i663[7]
  i662.deAudioEnabled = !!i663[8]
  i662.deUnityExtendedEnabled = !!i663[9]
  i662.epoOutlineEnabled = !!i663[10]
  return i662
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Resources"] = function (request, data, root) {
  var i664 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Resources' )
  var i665 = data
  var i667 = i665[0]
  var i666 = []
  for(var i = 0; i < i667.length; i += 1) {
    i666.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Resources+File', i667[i + 0]) );
  }
  i664.files = i666
  i664.componentToPrefabIds = i665[1]
  return i664
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Resources+File"] = function (request, data, root) {
  var i670 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Resources+File' )
  var i671 = data
  i670.path = i671[0]
  request.r(i671[1], i671[2], 0, i670, 'unityObject')
  return i670
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings"] = function (request, data, root) {
  var i672 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings' )
  var i673 = data
  var i675 = i673[0]
  var i674 = []
  for(var i = 0; i < i675.length; i += 1) {
    i674.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+ScriptsExecutionOrder', i675[i + 0]) );
  }
  i672.scriptsExecutionOrder = i674
  var i677 = i673[1]
  var i676 = []
  for(var i = 0; i < i677.length; i += 1) {
    i676.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+SortingLayer', i677[i + 0]) );
  }
  i672.sortingLayers = i676
  var i679 = i673[2]
  var i678 = []
  for(var i = 0; i < i679.length; i += 1) {
    i678.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+CullingLayer', i679[i + 0]) );
  }
  i672.cullingLayers = i678
  i672.timeSettings = request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+TimeSettings', i673[3], i672.timeSettings)
  i672.physicsSettings = request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings', i673[4], i672.physicsSettings)
  i672.physics2DSettings = request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings', i673[5], i672.physics2DSettings)
  i672.qualitySettings = request.d('Luna.Unity.DTO.UnityEngine.Assets.QualitySettings', i673[6], i672.qualitySettings)
  i672.enableRealtimeShadows = !!i673[7]
  i672.enableAutoInstancing = !!i673[8]
  i672.enableStaticBatching = !!i673[9]
  i672.enableDynamicBatching = !!i673[10]
  i672.lightmapEncodingQuality = i673[11]
  i672.desiredColorSpace = i673[12]
  var i681 = i673[13]
  var i680 = []
  for(var i = 0; i < i681.length; i += 1) {
    i680.push( i681[i + 0] );
  }
  i672.allTags = i680
  return i672
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+ScriptsExecutionOrder"] = function (request, data, root) {
  var i684 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+ScriptsExecutionOrder' )
  var i685 = data
  i684.name = i685[0]
  i684.value = i685[1]
  return i684
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+SortingLayer"] = function (request, data, root) {
  var i688 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+SortingLayer' )
  var i689 = data
  i688.id = i689[0]
  i688.name = i689[1]
  i688.value = i689[2]
  return i688
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+CullingLayer"] = function (request, data, root) {
  var i692 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+CullingLayer' )
  var i693 = data
  i692.id = i693[0]
  i692.name = i693[1]
  return i692
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+TimeSettings"] = function (request, data, root) {
  var i694 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+TimeSettings' )
  var i695 = data
  i694.fixedDeltaTime = i695[0]
  i694.maximumDeltaTime = i695[1]
  i694.timeScale = i695[2]
  i694.maximumParticleTimestep = i695[3]
  return i694
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings"] = function (request, data, root) {
  var i696 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings' )
  var i697 = data
  i696.gravity = new pc.Vec3( i697[0], i697[1], i697[2] )
  i696.defaultSolverIterations = i697[3]
  i696.bounceThreshold = i697[4]
  i696.autoSyncTransforms = !!i697[5]
  i696.autoSimulation = !!i697[6]
  var i699 = i697[7]
  var i698 = []
  for(var i = 0; i < i699.length; i += 1) {
    i698.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings+CollisionMask', i699[i + 0]) );
  }
  i696.collisionMatrix = i698
  return i696
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings+CollisionMask"] = function (request, data, root) {
  var i702 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings+CollisionMask' )
  var i703 = data
  i702.enabled = !!i703[0]
  i702.layerId = i703[1]
  i702.otherLayerId = i703[2]
  return i702
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings"] = function (request, data, root) {
  var i704 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings' )
  var i705 = data
  request.r(i705[0], i705[1], 0, i704, 'material')
  i704.gravity = new pc.Vec2( i705[2], i705[3] )
  i704.positionIterations = i705[4]
  i704.velocityIterations = i705[5]
  i704.velocityThreshold = i705[6]
  i704.maxLinearCorrection = i705[7]
  i704.maxAngularCorrection = i705[8]
  i704.maxTranslationSpeed = i705[9]
  i704.maxRotationSpeed = i705[10]
  i704.baumgarteScale = i705[11]
  i704.baumgarteTOIScale = i705[12]
  i704.timeToSleep = i705[13]
  i704.linearSleepTolerance = i705[14]
  i704.angularSleepTolerance = i705[15]
  i704.defaultContactOffset = i705[16]
  i704.autoSimulation = !!i705[17]
  i704.queriesHitTriggers = !!i705[18]
  i704.queriesStartInColliders = !!i705[19]
  i704.callbacksOnDisable = !!i705[20]
  i704.reuseCollisionCallbacks = !!i705[21]
  i704.autoSyncTransforms = !!i705[22]
  var i707 = i705[23]
  var i706 = []
  for(var i = 0; i < i707.length; i += 1) {
    i706.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings+CollisionMask', i707[i + 0]) );
  }
  i704.collisionMatrix = i706
  return i704
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings+CollisionMask"] = function (request, data, root) {
  var i710 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings+CollisionMask' )
  var i711 = data
  i710.enabled = !!i711[0]
  i710.layerId = i711[1]
  i710.otherLayerId = i711[2]
  return i710
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.QualitySettings"] = function (request, data, root) {
  var i712 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.QualitySettings' )
  var i713 = data
  var i715 = i713[0]
  var i714 = []
  for(var i = 0; i < i715.length; i += 1) {
    i714.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.QualitySettings', i715[i + 0]) );
  }
  i712.qualityLevels = i714
  var i717 = i713[1]
  var i716 = []
  for(var i = 0; i < i717.length; i += 1) {
    i716.push( i717[i + 0] );
  }
  i712.names = i716
  i712.shadows = i713[2]
  i712.anisotropicFiltering = i713[3]
  i712.antiAliasing = i713[4]
  i712.lodBias = i713[5]
  i712.shadowCascades = i713[6]
  i712.shadowDistance = i713[7]
  i712.shadowmaskMode = i713[8]
  i712.shadowProjection = i713[9]
  i712.shadowResolution = i713[10]
  i712.softParticles = !!i713[11]
  i712.softVegetation = !!i713[12]
  i712.activeColorSpace = i713[13]
  i712.desiredColorSpace = i713[14]
  i712.masterTextureLimit = i713[15]
  i712.maxQueuedFrames = i713[16]
  i712.particleRaycastBudget = i713[17]
  i712.pixelLightCount = i713[18]
  i712.realtimeReflectionProbes = !!i713[19]
  i712.shadowCascade2Split = i713[20]
  i712.shadowCascade4Split = new pc.Vec3( i713[21], i713[22], i713[23] )
  i712.streamingMipmapsActive = !!i713[24]
  i712.vSyncCount = i713[25]
  i712.asyncUploadBufferSize = i713[26]
  i712.asyncUploadTimeSlice = i713[27]
  i712.billboardsFaceCameraPosition = !!i713[28]
  i712.shadowNearPlaneOffset = i713[29]
  i712.streamingMipmapsMemoryBudget = i713[30]
  i712.maximumLODLevel = i713[31]
  i712.streamingMipmapsAddAllCameras = !!i713[32]
  i712.streamingMipmapsMaxLevelReduction = i713[33]
  i712.streamingMipmapsRenderersPerFrame = i713[34]
  i712.resolutionScalingFixedDPIFactor = i713[35]
  i712.streamingMipmapsMaxFileIORequests = i713[36]
  i712.currentQualityLevel = i713[37]
  return i712
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Mesh+BlendShapeFrame"] = function (request, data, root) {
  var i722 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Mesh+BlendShapeFrame' )
  var i723 = data
  i722.weight = i723[0]
  i722.vertices = i723[1]
  i722.normals = i723[2]
  i722.tangents = i723[3]
  return i722
}

Deserializers.fields = {"Luna.Unity.DTO.UnityEngine.Components.Transform":{"position":0,"scale":3,"rotation":6},"Luna.Unity.DTO.UnityEngine.Components.MeshFilter":{"sharedMesh":0},"Luna.Unity.DTO.UnityEngine.Components.MeshRenderer":{"additionalVertexStreams":0,"enabled":2,"sharedMaterial":3,"sharedMaterials":5,"receiveShadows":6,"shadowCastingMode":7,"sortingLayerID":8,"sortingOrder":9,"lightmapIndex":10,"lightmapSceneIndex":11,"lightmapScaleOffset":12,"lightProbeUsage":16,"reflectionProbeUsage":17},"Luna.Unity.DTO.UnityEngine.Scene.GameObject":{"name":0,"tagId":1,"enabled":2,"isStatic":3,"layer":4},"Luna.Unity.DTO.UnityEngine.Assets.Mesh":{"name":0,"halfPrecision":1,"useSimplification":2,"useUInt32IndexFormat":3,"vertexCount":4,"aabb":5,"streams":6,"vertices":7,"subMeshes":8,"bindposes":9,"blendShapes":10},"Luna.Unity.DTO.UnityEngine.Assets.Mesh+SubMesh":{"triangles":0},"Luna.Unity.DTO.UnityEngine.Assets.Mesh+BlendShape":{"name":0,"frames":1},"Luna.Unity.DTO.UnityEngine.Assets.Material":{"name":0,"shader":1,"renderQueue":3,"enableInstancing":4,"floatParameters":5,"colorParameters":6,"vectorParameters":7,"textureParameters":8,"materialFlags":9},"Luna.Unity.DTO.UnityEngine.Assets.Material+FloatParameter":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.Material+ColorParameter":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.Material+VectorParameter":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.Material+TextureParameter":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.Material+MaterialFlag":{"name":0,"enabled":1},"Luna.Unity.DTO.UnityEngine.Components.ParticleSystem":{"main":0,"colorBySpeed":1,"colorOverLifetime":2,"emission":3,"rotationBySpeed":4,"rotationOverLifetime":5,"shape":6,"sizeBySpeed":7,"sizeOverLifetime":8,"textureSheetAnimation":9,"velocityOverLifetime":10,"noise":11,"inheritVelocity":12,"forceOverLifetime":13,"limitVelocityOverLifetime":14,"useAutoRandomSeed":15,"randomSeed":16},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.MainModule":{"duration":0,"loop":1,"prewarm":2,"startDelay":3,"startLifetime":4,"startSpeed":5,"startSize3D":6,"startSizeX":7,"startSizeY":8,"startSizeZ":9,"startRotation3D":10,"startRotationX":11,"startRotationY":12,"startRotationZ":13,"startColor":14,"gravityModifier":15,"simulationSpace":16,"customSimulationSpace":17,"simulationSpeed":19,"useUnscaledTime":20,"scalingMode":21,"playOnAwake":22,"maxParticles":23,"emitterVelocityMode":24,"stopAction":25},"Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve":{"mode":0,"curveMin":1,"curveMax":2,"curveMultiplier":3,"constantMin":4,"constantMax":5},"Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxGradient":{"mode":0,"gradientMin":1,"gradientMax":2,"colorMin":3,"colorMax":7},"Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Gradient":{"mode":0,"colorKeys":1,"alphaKeys":2},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ColorBySpeedModule":{"enabled":0,"color":1,"range":2},"Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientColorKey":{"color":0,"time":4},"Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientAlphaKey":{"alpha":0,"time":1},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ColorOverLifetimeModule":{"enabled":0,"color":1},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.EmissionModule":{"enabled":0,"rateOverTime":1,"rateOverDistance":2,"bursts":3},"Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Burst":{"count":0,"cycleCount":1,"minCount":2,"maxCount":3,"repeatInterval":4,"time":5},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.RotationBySpeedModule":{"enabled":0,"x":1,"y":2,"z":3,"separateAxes":4,"range":5},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.RotationOverLifetimeModule":{"enabled":0,"x":1,"y":2,"z":3,"separateAxes":4},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ShapeModule":{"enabled":0,"shapeType":1,"randomDirectionAmount":2,"sphericalDirectionAmount":3,"randomPositionAmount":4,"alignToDirection":5,"radius":6,"radiusMode":7,"radiusSpread":8,"radiusSpeed":9,"radiusThickness":10,"angle":11,"length":12,"boxThickness":13,"meshShapeType":16,"mesh":17,"meshRenderer":19,"skinnedMeshRenderer":21,"useMeshMaterialIndex":23,"meshMaterialIndex":24,"useMeshColors":25,"normalOffset":26,"arc":27,"arcMode":28,"arcSpread":29,"arcSpeed":30,"donutRadius":31,"position":32,"rotation":35,"scale":38},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.SizeBySpeedModule":{"enabled":0,"x":1,"y":2,"z":3,"separateAxes":4,"range":5},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.SizeOverLifetimeModule":{"enabled":0,"x":1,"y":2,"z":3,"separateAxes":4},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.TextureSheetAnimationModule":{"enabled":0,"mode":1,"animation":2,"numTilesX":3,"numTilesY":4,"useRandomRow":5,"frameOverTime":6,"startFrame":7,"cycleCount":8,"rowIndex":9,"flipU":10,"flipV":11,"spriteCount":12,"sprites":13},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.VelocityOverLifetimeModule":{"enabled":0,"x":1,"y":2,"z":3,"radial":4,"speedModifier":5,"space":6,"orbitalX":7,"orbitalY":8,"orbitalZ":9,"orbitalOffsetX":10,"orbitalOffsetY":11,"orbitalOffsetZ":12},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.NoiseModule":{"enabled":0,"separateAxes":1,"strengthX":2,"strengthY":3,"strengthZ":4,"frequency":5,"damping":6,"octaveCount":7,"octaveMultiplier":8,"octaveScale":9,"quality":10,"scrollSpeed":11,"scrollSpeedMultiplier":12,"remapEnabled":13,"remapX":14,"remapY":15,"remapZ":16,"positionAmount":17,"rotationAmount":18,"sizeAmount":19},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.InheritVelocityModule":{"enabled":0,"mode":1,"curve":2},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ForceOverLifetimeModule":{"enabled":0,"x":1,"y":2,"z":3,"space":4,"randomized":5},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.LimitVelocityOverLifetimeModule":{"enabled":0,"limit":1,"limitX":2,"limitY":3,"limitZ":4,"dampen":5,"separateAxes":6,"space":7,"drag":8,"multiplyDragByParticleSize":9,"multiplyDragByParticleVelocity":10},"Luna.Unity.DTO.UnityEngine.Components.ParticleSystemRenderer":{"mesh":0,"meshCount":2,"activeVertexStreamsCount":3,"alignment":4,"renderMode":5,"sortMode":6,"lengthScale":7,"velocityScale":8,"cameraVelocityScale":9,"normalDirection":10,"sortingFudge":11,"minParticleSize":12,"maxParticleSize":13,"pivot":14,"trailMaterial":17,"applyActiveColorSpace":19,"enabled":20,"sharedMaterial":21,"sharedMaterials":23,"receiveShadows":24,"shadowCastingMode":25,"sortingLayerID":26,"sortingOrder":27,"lightmapIndex":28,"lightmapSceneIndex":29,"lightmapScaleOffset":30,"lightProbeUsage":34,"reflectionProbeUsage":35},"Luna.Unity.DTO.UnityEngine.Textures.Texture2D":{"name":0,"width":1,"height":2,"mipmapCount":3,"anisoLevel":4,"filterMode":5,"hdr":6,"format":7,"wrapMode":8,"alphaIsTransparency":9,"alphaSource":10,"graphicsFormat":11,"sRGBTexture":12,"desiredColorSpace":13,"wrapU":14,"wrapV":15},"Luna.Unity.DTO.UnityEngine.Components.SphereCollider":{"center":0,"radius":3,"enabled":4,"isTrigger":5,"material":6},"Luna.Unity.DTO.UnityEngine.Textures.Cubemap":{"name":0,"atlasId":1,"mipmapCount":2,"hdr":3,"size":4,"anisoLevel":5,"filterMode":6,"rects":7,"wrapU":8,"wrapV":9},"Luna.Unity.DTO.UnityEngine.Scene.Scene":{"name":0,"index":1,"startup":2},"Luna.Unity.DTO.UnityEngine.Components.Camera":{"aspect":0,"orthographic":1,"orthographicSize":2,"backgroundColor":3,"nearClipPlane":7,"farClipPlane":8,"fieldOfView":9,"depth":10,"clearFlags":11,"cullingMask":12,"rect":13,"targetTexture":14,"usePhysicalProperties":16,"focalLength":17,"sensorSize":18,"lensShift":20,"gateFit":22,"commandBufferCount":23,"cameraType":24,"enabled":25},"Luna.Unity.DTO.UnityEngine.Components.Light":{"type":0,"color":1,"cullingMask":5,"intensity":6,"range":7,"spotAngle":8,"shadows":9,"shadowNormalBias":10,"shadowBias":11,"shadowStrength":12,"shadowResolution":13,"lightmapBakeType":14,"renderMode":15,"cookie":16,"cookieSize":18,"shadowNearPlane":19,"enabled":20},"Luna.Unity.DTO.UnityEngine.Assets.RenderSettings":{"ambientIntensity":0,"reflectionIntensity":1,"ambientMode":2,"ambientLight":3,"ambientSkyColor":7,"ambientGroundColor":11,"ambientEquatorColor":15,"fogColor":19,"fogEndDistance":23,"fogStartDistance":24,"fogDensity":25,"fog":26,"skybox":27,"fogMode":29,"lightmaps":30,"lightProbes":31,"lightmapsMode":32,"mixedBakeMode":33,"environmentLightingMode":34,"ambientProbe":35,"referenceAmbientProbe":36,"useReferenceAmbientProbe":37,"customReflection":38,"defaultReflection":40,"defaultReflectionMode":42,"defaultReflectionResolution":43,"sunLightObjectId":44,"pixelLightCount":45,"defaultReflectionHDR":46,"hasLightDataAsset":47,"hasManualGenerate":48},"Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+Lightmap":{"lightmapColor":0,"lightmapDirection":2},"Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+LightProbes":{"bakedProbes":0,"positions":1,"hullRays":2,"tetrahedra":3,"neighbours":4,"matrices":5},"Luna.Unity.DTO.UnityEngine.Assets.UniversalRenderPipelineAsset":{"AdditionalLightsPerObjectLimit":0,"AdditionalLightsRenderingMode":1,"LightRenderingMode":2,"ColorGradingLutSize":3,"ColorGradingMode":4,"MainLightRenderingMode":5,"MainLightRenderingModeValue":6,"SupportsMainLightShadows":7,"MixedLightingSupported":8,"MsaaQuality":9,"MSAA":10,"OpaqueDownsampling":11,"MainLightShadowmapResolution":12,"MainLightShadowmapResolutionValue":13,"SupportsSoftShadows":14,"SoftShadowQuality":15,"SoftShadowQualityValue":16,"ShadowDistance":17,"ShadowCascadeCount":18,"Cascade2Split":19,"Cascade3Split":20,"Cascade4Split":22,"CascadeBorder":25,"ShadowDepthBias":26,"ShadowNormalBias":27,"RenderScale":28,"RequireDepthTexture":29,"RequireOpaqueTexture":30,"SupportsHDR":31,"SupportsTerrainHoles":32},"Luna.Unity.DTO.UnityEngine.Assets.LightRenderingMode":{"Disabled":0,"PerVertex":1,"PerPixel":2},"Luna.Unity.DTO.UnityEngine.Assets.ColorGradingMode":{"LowDynamicRange":0,"HighDynamicRange":1},"Luna.Unity.DTO.UnityEngine.Assets.MsaaQuality":{"Disabled":0,"_2x":1,"_4x":2,"_8x":3},"Luna.Unity.DTO.UnityEngine.Assets.Downsampling":{"None":0,"_2xBilinear":1,"_4xBox":2,"_4xBilinear":3},"Luna.Unity.DTO.UnityEngine.Assets.ShadowResolution":{"_256":0,"_512":1,"_1024":2,"_2048":3,"_4096":4},"Luna.Unity.DTO.UnityEngine.Assets.SoftShadowQuality":{"UsePipelineSettings":0,"Low":1,"Medium":2,"High":3},"Luna.Unity.DTO.UnityEngine.Assets.Shader":{"ShaderCompilationErrors":0,"name":1,"guid":2,"shaderDefinedKeywords":3,"passes":4,"usePasses":5,"defaultParameterValues":6,"unityFallbackShader":7,"readDepth":9,"hasDepthOnlyPass":10,"isCreatedByShaderGraph":11,"disableBatching":12,"compiled":13},"Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError":{"shaderName":0,"errorMessage":1},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass":{"id":0,"subShaderIndex":1,"name":2,"passType":3,"grabPassTextureName":4,"usePass":5,"zTest":6,"zWrite":7,"culling":8,"blending":9,"alphaBlending":10,"colorWriteMask":11,"offsetUnits":12,"offsetFactor":13,"stencilRef":14,"stencilReadMask":15,"stencilWriteMask":16,"stencilOp":17,"stencilOpFront":18,"stencilOpBack":19,"tags":20,"passDefinedKeywords":21,"passDefinedKeywordGroups":22,"variants":23,"excludedVariants":24,"hasDepthReader":25},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value":{"val":0,"name":1},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending":{"src":0,"dst":1,"op":2},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp":{"pass":0,"fail":1,"zFail":2,"comp":3},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Tag":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+KeywordGroup":{"keywords":0,"hasDiscard":1},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant":{"passId":0,"subShaderIndex":1,"keywords":2,"vertexProgram":3,"fragmentProgram":4,"exportedForWebGl2":5,"readDepth":6},"Luna.Unity.DTO.UnityEngine.Assets.Shader+UsePass":{"shader":0,"pass":2},"Luna.Unity.DTO.UnityEngine.Assets.Shader+DefaultParameterValue":{"name":0,"type":1,"value":2,"textureValue":6,"shaderPropertyFlag":7},"Luna.Unity.DTO.UnityEngine.Assets.Resources":{"files":0,"componentToPrefabIds":1},"Luna.Unity.DTO.UnityEngine.Assets.Resources+File":{"path":0,"unityObject":1},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings":{"scriptsExecutionOrder":0,"sortingLayers":1,"cullingLayers":2,"timeSettings":3,"physicsSettings":4,"physics2DSettings":5,"qualitySettings":6,"enableRealtimeShadows":7,"enableAutoInstancing":8,"enableStaticBatching":9,"enableDynamicBatching":10,"lightmapEncodingQuality":11,"desiredColorSpace":12,"allTags":13},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+ScriptsExecutionOrder":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+SortingLayer":{"id":0,"name":1,"value":2},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+CullingLayer":{"id":0,"name":1},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+TimeSettings":{"fixedDeltaTime":0,"maximumDeltaTime":1,"timeScale":2,"maximumParticleTimestep":3},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings":{"gravity":0,"defaultSolverIterations":3,"bounceThreshold":4,"autoSyncTransforms":5,"autoSimulation":6,"collisionMatrix":7},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings+CollisionMask":{"enabled":0,"layerId":1,"otherLayerId":2},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings":{"material":0,"gravity":2,"positionIterations":4,"velocityIterations":5,"velocityThreshold":6,"maxLinearCorrection":7,"maxAngularCorrection":8,"maxTranslationSpeed":9,"maxRotationSpeed":10,"baumgarteScale":11,"baumgarteTOIScale":12,"timeToSleep":13,"linearSleepTolerance":14,"angularSleepTolerance":15,"defaultContactOffset":16,"autoSimulation":17,"queriesHitTriggers":18,"queriesStartInColliders":19,"callbacksOnDisable":20,"reuseCollisionCallbacks":21,"autoSyncTransforms":22,"collisionMatrix":23},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings+CollisionMask":{"enabled":0,"layerId":1,"otherLayerId":2},"Luna.Unity.DTO.UnityEngine.Assets.QualitySettings":{"qualityLevels":0,"names":1,"shadows":2,"anisotropicFiltering":3,"antiAliasing":4,"lodBias":5,"shadowCascades":6,"shadowDistance":7,"shadowmaskMode":8,"shadowProjection":9,"shadowResolution":10,"softParticles":11,"softVegetation":12,"activeColorSpace":13,"desiredColorSpace":14,"masterTextureLimit":15,"maxQueuedFrames":16,"particleRaycastBudget":17,"pixelLightCount":18,"realtimeReflectionProbes":19,"shadowCascade2Split":20,"shadowCascade4Split":21,"streamingMipmapsActive":24,"vSyncCount":25,"asyncUploadBufferSize":26,"asyncUploadTimeSlice":27,"billboardsFaceCameraPosition":28,"shadowNearPlaneOffset":29,"streamingMipmapsMemoryBudget":30,"maximumLODLevel":31,"streamingMipmapsAddAllCameras":32,"streamingMipmapsMaxLevelReduction":33,"streamingMipmapsRenderersPerFrame":34,"resolutionScalingFixedDPIFactor":35,"streamingMipmapsMaxFileIORequests":36,"currentQualityLevel":37},"Luna.Unity.DTO.UnityEngine.Assets.Mesh+BlendShapeFrame":{"weight":0,"vertices":1,"normals":2,"tangents":3}}

Deserializers.requiredComponents = {"27":[28],"29":[28],"30":[28],"31":[28],"32":[28],"33":[28],"34":[35],"36":[15],"37":[38],"39":[38],"40":[38],"41":[38],"42":[38],"43":[38],"44":[38],"45":[46],"47":[46],"48":[46],"49":[46],"50":[46],"51":[46],"52":[46],"53":[46],"54":[46],"55":[46],"56":[46],"57":[46],"58":[46],"59":[15],"60":[3],"61":[62],"63":[62],"64":[65],"13":[66],"67":[65],"68":[15],"17":[15],"19":[18],"69":[70],"71":[65],"72":[65],"73":[64],"74":[75,65],"76":[65],"77":[64],"78":[65],"79":[65],"80":[65],"81":[65],"82":[65],"83":[65],"84":[65],"85":[65],"86":[65],"87":[75,65],"88":[65],"89":[65],"90":[65],"91":[65],"92":[75,65],"93":[65],"94":[95],"96":[95],"97":[95],"98":[95],"99":[15],"100":[15],"101":[70],"102":[95],"103":[64],"104":[65],"105":[3,65],"106":[65,75],"107":[65],"108":[75,65],"109":[3],"110":[75,65],"111":[65],"112":[70]}

Deserializers.types = ["UnityEngine.Transform","UnityEngine.MeshFilter","UnityEngine.Mesh","UnityEngine.MeshRenderer","UnityEngine.Material","UnityEngine.MonoBehaviour","HexCellView","UnityEngine.Shader","TileStackView","TileConfig","UnityEngine.ParticleSystem","UnityEngine.ParticleSystemRenderer","UnityEngine.Texture2D","TileStackRoot","UnityEngine.SphereCollider","UnityEngine.Camera","UnityEngine.AudioListener","UnityEngine.Rendering.Universal.UniversalAdditionalCameraData","UnityEngine.Light","UnityEngine.Rendering.Universal.UniversalAdditionalLightData","UnityEngine.Grid","GameplayEntry","TileStackSpawnPoint","TileStackDragInput","GridDefinitionConfig","UnityEngine.Cubemap","DG.Tweening.Core.DOTweenSettings","UnityEngine.AudioLowPassFilter","UnityEngine.AudioBehaviour","UnityEngine.AudioHighPassFilter","UnityEngine.AudioReverbFilter","UnityEngine.AudioDistortionFilter","UnityEngine.AudioEchoFilter","UnityEngine.AudioChorusFilter","UnityEngine.Cloth","UnityEngine.SkinnedMeshRenderer","UnityEngine.FlareLayer","UnityEngine.ConstantForce","UnityEngine.Rigidbody","UnityEngine.Joint","UnityEngine.HingeJoint","UnityEngine.SpringJoint","UnityEngine.FixedJoint","UnityEngine.CharacterJoint","UnityEngine.ConfigurableJoint","UnityEngine.CompositeCollider2D","UnityEngine.Rigidbody2D","UnityEngine.Joint2D","UnityEngine.AnchoredJoint2D","UnityEngine.SpringJoint2D","UnityEngine.DistanceJoint2D","UnityEngine.FrictionJoint2D","UnityEngine.HingeJoint2D","UnityEngine.RelativeJoint2D","UnityEngine.SliderJoint2D","UnityEngine.TargetJoint2D","UnityEngine.FixedJoint2D","UnityEngine.WheelJoint2D","UnityEngine.ConstantForce2D","UnityEngine.StreamingController","UnityEngine.TextMesh","UnityEngine.Tilemaps.TilemapRenderer","UnityEngine.Tilemaps.Tilemap","UnityEngine.Tilemaps.TilemapCollider2D","UnityEngine.Canvas","UnityEngine.RectTransform","UnityEngine.Collider","UnityEngine.Rendering.UI.UIFoldout","UnityEngine.Experimental.Rendering.Universal.PixelPerfectCamera","Unity.VisualScripting.SceneVariables","Unity.VisualScripting.Variables","UnityEngine.UI.Dropdown","UnityEngine.UI.Graphic","UnityEngine.UI.GraphicRaycaster","UnityEngine.UI.Image","UnityEngine.CanvasRenderer","UnityEngine.UI.AspectRatioFitter","UnityEngine.UI.CanvasScaler","UnityEngine.UI.ContentSizeFitter","UnityEngine.UI.GridLayoutGroup","UnityEngine.UI.HorizontalLayoutGroup","UnityEngine.UI.HorizontalOrVerticalLayoutGroup","UnityEngine.UI.LayoutElement","UnityEngine.UI.LayoutGroup","UnityEngine.UI.VerticalLayoutGroup","UnityEngine.UI.Mask","UnityEngine.UI.MaskableGraphic","UnityEngine.UI.RawImage","UnityEngine.UI.RectMask2D","UnityEngine.UI.Scrollbar","UnityEngine.UI.ScrollRect","UnityEngine.UI.Slider","UnityEngine.UI.Text","UnityEngine.UI.Toggle","UnityEngine.EventSystems.BaseInputModule","UnityEngine.EventSystems.EventSystem","UnityEngine.EventSystems.PointerInputModule","UnityEngine.EventSystems.StandaloneInputModule","UnityEngine.EventSystems.TouchInputModule","UnityEngine.EventSystems.Physics2DRaycaster","UnityEngine.EventSystems.PhysicsRaycaster","Unity.VisualScripting.ScriptMachine","UnityEngine.InputSystem.UI.InputSystemUIInputModule","UnityEngine.InputSystem.UI.TrackedDeviceRaycaster","TMPro.TextContainer","TMPro.TextMeshPro","TMPro.TextMeshProUGUI","TMPro.TMP_Dropdown","TMPro.TMP_SelectionCaret","TMPro.TMP_SubMesh","TMPro.TMP_SubMeshUI","TMPro.TMP_Text","Unity.VisualScripting.StateMachine"]

Deserializers.unityVersion = "2022.3.62f2";

Deserializers.productName = "test_3d";

Deserializers.lunaInitializationTime = "12/03/2025 22:18:18";

Deserializers.lunaDaysRunning = "0.6";

Deserializers.lunaVersion = "7.0.0";

Deserializers.lunaSHA = "3bcc3e343f23b4c67e768a811a8d088c7f7adbc5";

Deserializers.creativeName = "test_3d";

Deserializers.lunaAppID = "35041";

Deserializers.projectId = "9db75f8e0a880a448be84f3a7a50b82c";

Deserializers.packagesInfo = "com.unity.inputsystem: 1.14.2\ncom.unity.render-pipelines.universal: 14.0.12\ncom.unity.textmeshpro: 3.0.7\ncom.unity.timeline: 1.7.7\ncom.unity.ugui: 1.0.0";

Deserializers.externalJsLibraries = "";

Deserializers.androidLink = ( typeof window !== "undefined")&&window.$environment.packageConfig.androidLink?window.$environment.packageConfig.androidLink:'Empty';

Deserializers.iosLink = ( typeof window !== "undefined")&&window.$environment.packageConfig.iosLink?window.$environment.packageConfig.iosLink:'Empty';

Deserializers.base64Enabled = "False";

Deserializers.minifyEnabled = "True";

Deserializers.isForceUncompressed = "False";

Deserializers.isAntiAliasingEnabled = "False";

Deserializers.isRuntimeAnalysisEnabledForCode = "True";

Deserializers.runtimeAnalysisExcludedClassesCount = "1881";

Deserializers.runtimeAnalysisExcludedMethodsCount = "4445";

Deserializers.runtimeAnalysisExcludedModules = "physics2d, particle-system, reflection, prefabs, mecanim-wasm";

Deserializers.isRuntimeAnalysisEnabledForShaders = "True";

Deserializers.isRealtimeShadowsEnabled = "False";

Deserializers.isReferenceAmbientProbeBaked = "False";

Deserializers.isLunaCompilerV2Used = "True";

Deserializers.companyName = "DefaultCompany";

Deserializers.buildPlatform = "WebGL";

Deserializers.applicationIdentifier = "com.DefaultCompany.test-3d";

Deserializers.disableAntiAliasing = true;

Deserializers.graphicsConstraint = 24;

Deserializers.linearColorSpace = true;

Deserializers.buildID = "efb1bcc2-5491-4449-b2d0-6a1640618706";

Deserializers.runtimeInitializeOnLoadInfos = [[["UnityEngine","Rendering","DebugUpdater","RuntimeInit"],["UnityEngine","Experimental","Rendering","ScriptableRuntimeReflectionSystemSettings","ScriptingDirtyReflectionSystemInstance"]],[["UnityEngine","InputSystem","InputSystem","RunInitialUpdate"],["Unity","VisualScripting","RuntimeVSUsageUtility","RuntimeInitializeOnLoadBeforeSceneLoad"]],[],[["UnityEngine","Experimental","Rendering","XRSystem","XRSystemInit"]],[["UnityEngine","InputSystem","UI","InputSystemUIInputModule","ResetDefaultActions"],["UnityEngine","InputSystem","InputSystem","RunInitializeInPlayer"]]];

Deserializers.typeNameToIdMap = function(){ var i = 0; return Deserializers.types.reduce( function( res, item ) { res[ item ] = i++; return res; }, {} ) }()

